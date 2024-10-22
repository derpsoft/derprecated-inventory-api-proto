﻿namespace Derprecated.Api.Test.Core
{
    using System;
    using System.Collections.Generic;
    using Api.Services;
    using Funq;
    using Models;
    using Models.Test;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Caching;
    using ServiceStack.Configuration;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using ServiceStack.Text;
    using ServiceStack.Validation;

    internal class TestAppHost : AppHostBase
    {
        internal const string BaseUri = "http://localhost:2001/";

        static TestAppHost()
        {
            new TestAppHost()
                .Init()
                .Start(BaseUri);
        }

        public TestAppHost() : base("Test", typeof (BaseService).GetAssembly())
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

            var appSettings = new AppSettings();
            container.Register(appSettings);

            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));
            container.Register(c => c.Resolve<IDbConnectionFactory>().Open());
            container.Register(Constants.TestUserSession);

            // Validators
            container.RegisterValidators(typeof (IAuditable).GetAssembly());

            // Auth
            container.Register<IUserAuthRepository>(c => new InMemoryAuthRepository());

            // Cache
            container.Register<ICacheClient>(new MemoryCacheClient());

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
            using (var ctx = container.Resolve<IDbConnectionFactory>().Open())
            {
                ctx.DropAndCreateTable<ApiKey>();
                ctx.DropAndCreateTable<Product>();
                ctx.DropAndCreateTable<ProductImage>();
                ctx.DropAndCreateTable<ProductTag>();
                ctx.DropAndCreateTable<Location>();
                ctx.DropAndCreateTable<InventoryTransaction>();
                ctx.DropAndCreateTable<UserPriceOverride>();
                ctx.DropAndCreateTable<Vendor>();
            }

            // Plugins
            Plugins.Add(new CorsFeature(allowCredentials: true, allowedHeaders: "Content-Type, X-Requested-With",
                allowOriginWhitelist:
                    new List<string>
                    {
                        "http://localhost:6307",
                        "http://localhost:8080",
                        "http://0.0.0.0:8080",
                        "http://0.0.0.0:3000"
                    },
                maxAge: 3600));
            Plugins.Add(new RegistrationFeature());
            Plugins.Add(new AuthFeature(
                () => new UserSession(),
                new IAuthProvider[]
                {
                    //                    new TwitterAuthProvider(appSettings),
                    new CredentialsAuthProvider(appSettings)
                    {
                        SkipPasswordVerificationForInProcessRequests = true
                    }
                },
                "/login"
                ));
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new AutoQueryFeature {MaxLimit = 100});

            var userRepo = container.Resolve<IUserAuthRepository>();
            var testUser = (IUserAuth) new UserAuth
            {
                Email = Constants.TestAuthenticate.UserName,
                Roles = new List<string>(new[] {Roles.User})
            };
            var testAdminUser = (IUserAuth) new UserAuth
            {
                Email = Constants.TestAdminAuthenticate.UserName,
                Roles = new List<string>(new[] {Roles.Admin}),
                Permissions = new List<string>(new[] {Permissions.CanDoEverything})
            };

            if (null == userRepo.GetUserAuthByUserName(testUser.Email))
                userRepo.CreateUserAuth(testUser, Constants.TestAuthenticate.Password);
            if (null == userRepo.GetUserAuthByUserName(testAdminUser.Email))
                userRepo.CreateUserAuth(testAdminUser, Constants.TestAdminAuthenticate.Password);

            // Misc
            container.Register(new ShopifyServiceClient($"https://{appSettings.Get("shopify.store.domain")}")
            {
                UserName = appSettings.Get("shopify.api.key"),
                Password = appSettings.Get("shopify.api.password")
            });
        }
    }
}
