using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using BausCode.Api.Models;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;
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
            // Settings
            SetConfig(new HostConfig());

            JsConfig.EmitCamelCaseNames = true;
            JsConfig.ExcludeTypeInfo = true;
            JsConfig.DateHandler = DateHandler.ISO8601;

            LogManager.LogFactory = new NLogFactory();

            var baseSettings = new AppSettings();
            container.Register(baseSettings);
            container.Register(c => new MultiAppSettings(c.Resolve<AppSettings>()));

            var appSettings = container.Resolve<MultiAppSettings>();


            // DB
            container.Register<IDbConnectionFactory>(c =>
            {
                var connectionString = ConfigurationManager.ConnectionStrings["AzureSql"].ConnectionString;
                return new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
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
            }
#if DEBUG
            var testUser = (IUserAuth) new UserAuth
            {
                Email = "james@bauscode.com"
            };
            var existing = userRepo.GetUserAuthByUserName(testUser.UserName);
            if (null == existing)
            {
                userRepo.CreateUserAuth(testUser, "12345");
            }
#endif

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

            container.Register(configuration);
        }
    }
}