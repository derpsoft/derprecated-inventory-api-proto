namespace Derprecated.Api.Configuration.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Reflection;
    using Funq;
    using MailKit.Net.Smtp;
    using Models;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Api.Swagger;
    using ServiceStack.Auth;
    using ServiceStack.Caching;
    using ServiceStack.Configuration;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using ServiceStack.Redis;
    using ServiceStack.Text;
    using ServiceStack.Validation;

    public class Application : AppHostBase
    {
        public Application(string applicationName, Assembly assembly)
            : base("Derprecated::Api::{0}".Fmt(applicationName), assembly)
        {
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

            var baseSettings = new AppSettings();
            container.Register(baseSettings);
            container.Register(c => new MultiAppSettings(c.Resolve<AppSettings>()));
            var appSettings = container.Resolve<MultiAppSettings>();
            var configuration =
                container.Resolve<Microsoft.Extensions.Options.IOptions<ApplicationConfiguration>>().Value;

            // DB
            container.Register<IDbConnectionFactory>(c =>
                                                     {
                                                         var connectionString =
                                                             configuration.ConnectionStrings.AzureSql;
                                                         return new OrmLiteConnectionFactory(connectionString,
                                                             SqlServerDialect.Provider);
                                                     });

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
            container.RegisterValidators(typeof (IAuditable).GetAssembly());

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
            var userRepo = (OrmLiteAuthRepository) container.Resolve<IUserAuthRepository>();
            userRepo.InitSchema();

            using (var ctx = container.Resolve<IDbConnectionFactory>().Open())
            {
                ctx.CreateTableIfNotExists<ApiKey>();
                ctx.CreateTableIfNotExists<Product>();
                ctx.CreateTableIfNotExists<ProductImage>();
                ctx.CreateTableIfNotExists<Tag>();
                ctx.CreateTableIfNotExists<ProductTag>();
                ctx.CreateTableIfNotExists<UserPriceOverride>();
                ctx.CreateTableIfNotExists<Vendor>();
                ctx.CreateTableIfNotExists<InventoryTransaction>();
                ctx.CreateTableIfNotExists<Sale>();
                ctx.CreateTableIfNotExists<Warehouse>();
                ctx.CreateTableIfNotExists<Location>();
            }
#if DEBUG
            var testUser = new UserAuth
                           {
                               Email = "james@derprecated.com"
                           };
            var existing = userRepo.GetUserAuthByUserName(testUser.Email);
            if (null == existing)
            {
                var newUser = userRepo.CreateUserAuth(testUser, "12345");
                userRepo.AssignRoles(newUser, new List<string> {Roles.Admin},
                    new List<string> {Permissions.CanDoEverything});
            }
#endif

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
            Plugins.Add(new CorsFeature(allowCredentials: true, allowedHeaders: "Content-Type, X-Requested-With",
                allowOriginWhitelist:
                    new List<string>
                    {
                        "http://localhost:6307",
                        "http://localhost:8080",
                        "http://0.0.0.0:8080",
                        "http://0.0.0.0:3000",
                        "http://inventory-web-dev-wb45gu.herokuapp.com",
                        "https://inventory-web-dev-wb45gu.herokuapp.com",
                        "http://inventory.derprecated.com",
                        "https://inventory.derprecated.com",
                        "http://inventory-web-pro.herokuapp.com",
                        "https://inventory-web-pro.herokuapp.com",
                    },
                maxAge: 3600));
            Plugins.Add(new RegistrationFeature());
            Plugins.Add(new AuthFeature(
                () => new UserSession(),
                new IAuthProvider[]
                {
                    new CredentialsAuthProvider()
                },
                "/login")
                        {
                            IncludeAssignRoleServices = true,
                            ValidateUniqueEmails = true
                        });
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new AutoQueryFeature {MaxLimit = 100});
            Plugins.Add(new SwaggerFeature());

            // Misc
            container.Register(new ShopifyServiceClient($"https://{configuration.Shopify.Domain}")
                               {
                                   UserName = configuration.Shopify.ApiKey,
                                   Password = configuration.Shopify.Password
                               });
        }
    }
}
