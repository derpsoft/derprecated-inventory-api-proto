namespace Derprecated.Api.Configuration.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Reflection;
    using Funq;
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Configuration;
    using Models;
    using ServiceStack;
    using ServiceStack.Auth;
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
            var configuration = container.Resolve<IConfigurationRoot>();

            // DB
            container.Register<IDbConnectionFactory>(c =>
                                                     {
                                                         var connectionString =
                                                             configuration.GetConnectionString("AzureSql");
                                                         return new OrmLiteConnectionFactory(connectionString,
                                                             SqlServerDialect.Provider);
                                                     });

            // Redis
            container.Register<IRedisClientsManager>(c =>
                                                     {
                                                         var connectionString =
                                                             configuration.GetConnectionString("AzureRedis");
                                                         return new RedisManagerPool(connectionString);
                                                     });

            // Cache
            container.Register(c => c.Resolve<IRedisClientsManager>().GetCacheClient());

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
                ctx.CreateTableIfNotExists<ProductTag>();
                ctx.CreateTableIfNotExists<UserPriceOverride>();
                ctx.CreateTableIfNotExists<Vendor>();
            }
#if DEBUG
            var testUser = new UserAuth
                           {
                               Email = "james@derprecated.com"
                           };
            var existing = userRepo.GetUserAuthByUserName(testUser.Email);
            if (null == existing)
            {
                userRepo.CreateUserAuth(testUser, "12345");
            }
#endif

            // Mail
            container.Register(c =>
                               {
                                   var host = appSettings.Get("mail.host");
                                   var port = appSettings.Get("mail.port", 587);
                                   var useSsl = appSettings.Get("mail.useSsl", true);
                                   var creds = new NetworkCredential(appSettings.Get("mail.username"),
                                       appSettings.Get("mail.password"));

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
                        "https://inventory-web-dev-wb45gu.herokuapp.com"
                    },
                maxAge: 3600));
            Plugins.Add(new RegistrationFeature());
            Plugins.Add(new AuthFeature(
                () => new UserSession(),
                new IAuthProvider[]
                {
                    new TwitterAuthProvider(appSettings),
                    new CredentialsAuthProvider(appSettings)
                },
                "/login")
                        {
                            IncludeAssignRoleServices = true,
                            ValidateUniqueEmails = true
                        });
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new AutoQueryFeature {MaxLimit = 100});

            // Misc
            container.Register(new ShopifyServiceClient($"https://{appSettings.Get("shopify.store.domain")}")
                               {
                                   UserName = appSettings.Get("shopify.api.key"),
                                   Password = appSettings.Get("shopify.api.password")
                               });
        }
    }
}
