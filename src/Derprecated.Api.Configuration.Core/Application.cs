namespace Derprecated.Api.Configuration.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Security.Cryptography;
    using Funq;
    using Handlers;
    using MailKit.Net.Smtp;
    using Microsoft.WindowsAzure.Storage;
    using Models;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Api.Swagger;
    using ServiceStack.Auth;
    using ServiceStack.Caching;
    using ServiceStack.Configuration;
    using ServiceStack.Data;
    using ServiceStack.Logging;
    using ServiceStack.OrmLite;
    using ServiceStack.Text;
    using ServiceStack.Validation;
    using ServiceStack.Stripe;
    using Auth0.AuthenticationApi;
    using Auth0.ManagementApi;

    public class Application : AppHostBase
    {
        public Application(string applicationName, Assembly assembly)
            : base("Derprecated::Api::{0}".Fmt(applicationName), assembly)
        {
        }

        private JsonWebKey JwkFromUri(Uri source)
        {
            using (var client = new HttpClient())
            {
                var raw = client.GetStringAsync(source);
                raw.Wait();
                var jwks = raw.Result.FromJson<JsonWebKeySet>();
                return jwks.Keys.First();
            }
        }

        private RSAParameters? RsaPubkeyFromJwk(JsonWebKey jwk)
        {
            return new RSAParameters
            {
                Modulus = jwk.N.FromBase64UrlSafe(),
                Exponent = jwk.E.FromBase64UrlSafe()
            };
        }

        public override void Configure(Container container)
        {
            // Settings
            SetConfig(new HostConfig {UseCamelCase = true});

            RoleNames.Admin = Roles.Admin;

            JsConfig.EmitCamelCaseNames = true;
            JsConfig.ExcludeTypeInfo = true;
            JsConfig<UserSession>.IncludeTypeInfo = true;
            JsConfig.DateHandler = DateHandler.ISO8601;

            LogManager.LogFactory = new ConsoleLogFactory(debugEnabled:true);

            var baseSettings = new AppSettings();
            container.Register(baseSettings);
            container.Register(c => new MultiAppSettings(c.Resolve<AppSettings>()));
            var appSettings = container.Resolve<MultiAppSettings>();
            var configuration =
                container.Resolve<Microsoft.Extensions.Options.IOptions<ApplicationConfiguration>>().Value;
            container.Register(configuration);

            // DB
            container.Register<IDbConnectionFactory>(c =>
            {
                var connectionString =
                    configuration.ConnectionStrings.AzureSql;
                return new OrmLiteConnectionFactory(connectionString,
                    SqlServerDialect.Provider);
            });
            container.Register(c => c.Resolve<IDbConnectionFactory>().Open())
                     .ReusedWithin(ReuseScope.None);

            // Redis
            //container.Register<IRedisClientsManager>(c =>
            //                                         {
            //                                             var connectionString =
            //                                                 configuration.ConnectionStrings.AzureRedis;
            //                                             return new RedisManagerPool(connectionString);
            //                                         });

            // Cache
            //container.Register(c => c.Resolve<IRedisClientsManager>().GetCacheClient());
            container.Register<ICacheClient>(new MemoryCacheClient {FlushOnDispose = false});

            // Validators
            container.RegisterValidators(typeof(IAuditable).GetAssembly());

            // Auth
            container.Register<IUserAuthRepository>(
                c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()) {UseDistinctRoleTables = true});

            // Db filters
            OrmLiteConfig.InsertFilter = (dbCmd, row) =>
            {
                if (row is IAuditable)
                {
                    var auditRow = row as IAuditable;
                    auditRow.CreateDate = auditRow.ModifyDate = DateTime.UtcNow;
                }

                if (row is Product)
                {
                    var product = row as Product;
                    product.OnInsert();
                }
            };
            OrmLiteConfig.UpdateFilter = (dbCmd, row) =>
            {
                if (row is IAuditable)
                {
                    var auditRow = row as IAuditable;
                    auditRow.ModifyDate = DateTime.UtcNow;

                    if (auditRow.CreateDate == DateTime.MinValue)
                        auditRow.CreateDate = auditRow.ModifyDate;
                }

                if (row is Product)
                {
                    var product = row as Product;
                    product.OnUpdate();
                }
            };

            // Schema init
            using (var ctx = container.Resolve<IDbConnectionFactory>().Open())
            {
                ctx.CreateTableIfNotExists<ApiKey>();
                ctx.CreateTableIfNotExists<Category>();
                ctx.CreateTableIfNotExists<Product>();
                ctx.CreateTableIfNotExists<ProductCategory>();
                ctx.CreateTableIfNotExists<ProductImage>();
                ctx.CreateTableIfNotExists<Tag>();
                ctx.CreateTableIfNotExists<ProductTag>();
                ctx.CreateTableIfNotExists<UserPriceOverride>();
                ctx.CreateTableIfNotExists<Vendor>();
                ctx.CreateTableIfNotExists<InventoryTransaction>();
                ctx.CreateTableIfNotExists<Sale>();
                ctx.CreateTableIfNotExists<Warehouse>();
                ctx.CreateTableIfNotExists<Location>();
                ctx.CreateTableIfNotExists<Image>();

#if DEBUG
                ctx.DropTable<Address>();
                ctx.DropTable<Offer>();
                ctx.DropTable<Order>();
                ctx.DropTable<Merchant>();
                ctx.DropTable<Customer>();
#endif

                ctx.CreateTableIfNotExists<Customer>();
                ctx.CreateTableIfNotExists<Merchant>();
                ctx.CreateTableIfNotExists<Order>();
                ctx.CreateTableIfNotExists<Offer>();
                ctx.CreateTableIfNotExists<Address>();
            }

            // Mail
            container.Register(c =>
            {
                var host = configuration.Mail.Host;
                var port = configuration.Mail.Port;
                var useSsl = configuration.Mail.UseSsl;
                var creds = new NetworkCredential(configuration.Mail.Username,
                    configuration.Mail.Password);

                var client = new SmtpClient();
                client.Connect(host, port, useSsl);
                client.Authenticate(creds);

                return client;
            });

            // Plugins
            Plugins.Add(new CorsFeature(
                allowCredentials: true,
                allowedMethods: "OPTIONS, GET, PUT, POST, PATCH, DELETE, SEARCH",
                allowedHeaders: "Content-Type, X-Requested-With, Cache-Control, Authorization",
                allowOriginWhitelist:
                new List<string>
                {
                    "http://localhost:6307",
                    "http://localhost:8080",
                    "http://0.0.0.0:8080",
                    "http://0.0.0.0:3000",
                    "http://inventory-web-dev-wb45gu.herokuapp.com",
                    "https://inventory-web-dev-wb45gu.herokuapp.com",
                    "https://inventory-web-sta-d8w373.herokuapp.com",
                    "https://inventory.derprecated.com",
                    "https://inventory-web-pro.herokuapp.com"
                },
                maxAge: 3600));

            var jwk = JwkFromUri(new Uri(configuration.Auth0.Jwks));
            var rsaPubkey = RsaPubkeyFromJwk(jwk);
            var managementUri = new Uri($"https://{configuration.Auth0.Domain}/");
            var tokenAuth = new JwtAuthProviderReader
            {
                RequireHashAlgorithm = true,
                HashAlgorithm = "RS256",
                PublicKey = rsaPubkey,
                Audience = configuration.Auth0.Audience,
                Issuer = configuration.Auth0.Issuer,
                PopulateSessionFilter = (session, json, request) =>
                {
                    var authorization = json.Object("app_metadata")
                      .Object("authorization");
                    session.Permissions = authorization
                      .GetArray<string>("permissions")
                      .ToList();
                    session.Roles = authorization
                      .GetArray<string>("roles")
                      .ToList();
                },
#if DEBUG
                RequireSecureConnection = false
#endif
            };
            Plugins.Add(new AuthFeature(
                () => new AuthUserSession(),
                new IAuthProvider[]
                {
                    tokenAuth
                },
                "/login")
            {
                IncludeAssignRoleServices = false,
                ValidateUniqueEmails = true
            });
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new AutoQueryFeature {MaxLimit = 100});
            Plugins.Add(new SwaggerFeature());

            // Handlers
            container.RegisterAs<ImageHandler, IHandler<Image>>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAs<ProductImageHandler, IHandler<ProductImage>>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAs<LocationHandler, IHandler<Location>>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAs<CategoryHandler, IHandler<Category>>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAs<VendorHandler, IHandler<Vendor>>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAs<WarehouseHandler, IHandler<Warehouse>>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAs<ProductHandler, IHandler<Product>>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWired<UserHandler>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAutoWired<Auth0Handler>();
            container.RegisterAs<AddressHandler, IHandler<Address>>()
                     .ReusedWithin(ReuseScope.Request);
            container.RegisterAs<OrderHandler, IHandler<Order>>()
                     .ReusedWithin(ReuseScope.Request);
            container.Register(new StripeHandler(configuration.Stripe.SecretKey));
            // Misc
            container.Register(new AuthenticationApiClient(new Uri($"https://{configuration.Auth0.Domain}/")));
            container.Register(new ShopifyServiceClient($"https://{configuration.Shopify.Domain}")
            {
                UserName = configuration.Shopify.ApiKey,
                Password = configuration.Shopify.Password
            });
            container.Register(c =>
            {
                var connectionString = configuration.ConnectionStrings.AzureStorage;
                return CloudStorageAccount.Parse(connectionString);
            });
            container.Register(c => c.Resolve<CloudStorageAccount>().CreateCloudBlobClient());
        }
    }
}
