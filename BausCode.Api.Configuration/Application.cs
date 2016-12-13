﻿namespace BausCode.Api.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using Funq;
    using Models;
    using ServiceStack;
    using ServiceStack.Admin;
    using ServiceStack.Auth;
    using ServiceStack.Configuration;
    using ServiceStack.Data;
    using ServiceStack.Logging;
    using ServiceStack.Logging.NLogger;
    using ServiceStack.OrmLite;
    using ServiceStack.Redis;
    using ServiceStack.Text;
    using ServiceStack.Validation;

    public class Application : AppHostBase
    {
        public Application(string applicationName, Assembly assembly)
            : base("BausCode::Api::{0}".Fmt(applicationName), assembly)
        {
        }

        public override ServiceStackHost Init()
        {
            var license = FindLicense();
            if (!license.IsNullOrEmpty())
            {
                Licensing.RegisterLicense(license);
            }

            return base.Init();
        }

        private string FindLicense()
        {
            var appSettings = new AppSettings();

            var license = Environment.GetEnvironmentVariable("ss.license") ?? appSettings.GetString("ss.license");

            return license;
        }

        public override void Configure(Container container)
        {
            // Settings
            SetConfig(new HostConfig());

            JsConfig.EmitCamelCaseNames = true;
            JsConfig.ExcludeTypeInfo = true;
            JsConfig<UserSession>.IncludeTypeInfo = true;
            JsConfig.DateHandler = DateHandler.ISO8601;
            LogManager.LogFactory = new NLogFactory();

            var baseSettings = new AppSettings();
            container.Register(baseSettings);
            container.Register(c => new MultiAppSettings(c.Resolve<AppSettings>()));

            var appSettings = container.Resolve<MultiAppSettings>();


            // DB
            container.Register<IDbConnectionFactory>(c =>
                                                     {
                                                         var connectionString =
                                                             ConfigurationManager.ConnectionStrings["AzureSql"]
                                                                 .ConnectionString;
                                                         return new OrmLiteConnectionFactory(connectionString,
                                                             SqlServerDialect.Provider);
                                                     });

            // Redis
            container.Register<IRedisClientsManager>(
                new RedisManagerPool(ConfigurationManager.ConnectionStrings["AzureRedis"].ConnectionString));

            // Validators
            container.RegisterValidators(typeof (IAuditable).Assembly);

            // Auth
            container.Register<IUserAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

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
            var testUser = (IUserAuth) new UserAuth
                                       {
                                           Email = "james@bauscode.com"
                                       };
            var existing = userRepo.GetUserAuthByUserName(testUser.Email);
            if (null == existing)
            {
                userRepo.CreateUserAuth(testUser, "12345");
            }
#endif

            // Mail
            container.Register(new SmtpClient
                               {
                                   Host = appSettings.Get("mail.host"),
                                   Port = appSettings.Get("mail.port", 587),
                                   Credentials =
                                       new NetworkCredential(appSettings.Get("mail.username"),
                                       appSettings.Get("mail.password")),
                                   EnableSsl = appSettings.Get("mail.useSsl", true)
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
                "/login"
                ));
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new AutoQueryFeature {MaxLimit = 100});
            Plugins.Add(new AdminFeature());

            // Misc
            container.Register(new ShopifyServiceClient($"https://{appSettings.Get("shopify.store.domain")}")
                               {
                                   UserName = appSettings.Get("shopify.api.key"),
                                   Password = appSettings.Get("shopify.api.password")
                               });
        }
    }
}
