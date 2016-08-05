using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using BausCode.Api.Models;
using BausCode.Api.Stores;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Text;
using ServiceStack.Validation;

namespace BausCode.Api.Configuration
{
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
            // DB
            container.Register<IDbConnectionFactory>(c =>
            {
                var connectionString = ConfigurationManager.ConnectionStrings["AzureSql"].ConnectionString;
                return new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            });

            // Redis
            container.Register<IRedisClientsManager>(
                new RedisManagerPool(ConfigurationManager.ConnectionStrings["AzureRedis"].ConnectionString));

            // Settings
            var baseSettings = new AppSettings();

            container.Register(baseSettings);
            container.RegisterAutoWired<OrmLiteAppSettings>();
            container.Register(c => new MultiAppSettings(c.Resolve<OrmLiteAppSettings>(), c.Resolve<AppSettings>()));

            var appSettings = container.Resolve<MultiAppSettings>();

            SetConfig(new HostConfig());

            JsConfig.EmitCamelCaseNames = true;
            JsConfig.ExcludeTypeInfo = true;
            JsConfig.DateHandler = DateHandler.ISO8601;
            
            // Validators
            container.RegisterValidators(typeof(IAuditable).Assembly);

            // Auth
            container.Register<IUserAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));
            container.RegisterAutoWiredAs<ApiKeyStore, IApiKeyStore>();

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
            var userRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
            var settings = container.Resolve<OrmLiteAppSettings>();

            try
            {
                userRepo.InitSchema();
                settings.InitSchema();
                using (var ctx = container.Resolve<IDbConnectionFactory>().Open())
                {
                    ctx.CreateTableIfNotExists<ApiKey>();
                }
#if DEBUG
                var testUser = (IUserAuth)new UserAuth
                {
                    Email = "james@bauscode.com"
                };
                var existing = userRepo.GetUserAuthByUserName(testUser.UserName);
                if (null == existing)
                {
                    testUser = userRepo.CreateUserAuth(testUser, "12345");

                    var apiKey = new ApiKey()
                    {
                        ApplicationName = "BausCode",
                        Key = Guid.NewGuid(),
                        UserAuthId = testUser.Id
                    };
                    using (var ctx = container.Resolve<IDbConnectionFactory>().Open())
                    {
                        ctx.Save(apiKey);
                    }
                }
#endif
            }
            catch (Exception)
            {
                // don't block website startup because of db connection failure, just assume that the subsequent calls will fail
            }


            // Plugins
            Plugins.Add(new CorsFeature(allowCredentials: true, allowedHeaders: "Content-Type, X-Requested-With",
                allowOriginWhitelist:
                    new List<string>
                    {
                        "http://localhost:6307",
                        "http://localhost:8080",
                        "http://0.0.0.0:8080",
                        "http://0.0.0.0:3000",
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

            // Configuration
            var configuration = new Models.Configuration();
            //configuration.QueueName = appSettings.Get("bc.jobs.opm.queueName");

            container.Register(configuration);
        }
    }
}
