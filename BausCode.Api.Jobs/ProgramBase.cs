using System;
using System.Configuration;
using BausCode.Api.Models;
using Funq;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace BausCode.Api.Jobs
{
    public abstract class ProgramBase
    {
        private readonly Container Container;

        protected ProgramBase()
        {
            Container = new Container();
        }


        protected void Init()
        {
            var license = FindLicense();
            if (!license.IsNullOrEmpty())
            {
                Licensing.RegisterLicense(license);
            }

            Configure(Container);
            Run();
        }

        private string FindLicense()
        {
            var appSettings = new AppSettings();

            var license = Environment.GetEnvironmentVariable("ss.license") ?? appSettings.GetString("ss.license");

            return license;
        }

        private void Run()
        {
            var job = Container.Resolve<IJob>();
            job.Run();
        }

        protected virtual void Configure(Container container)
        {
            // DB
            container.Register<IDbConnectionFactory>(c =>
            {
                var connectionString = ConfigurationManager.ConnectionStrings["AzureSql"].ConnectionString;
                return new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            });

            // Cache
            container.Register<ICacheClient>(new MemoryCacheClient());

            // Filters
            OrmLiteConfig.InsertFilter = (dbCmd, row) =>
            {
                var auditRow = row as IAuditable;
                if (null != auditRow)
                {
                    auditRow.CreateDate = auditRow.ModifyDate = DateTime.UtcNow;
                }
            };
            OrmLiteConfig.UpdateFilter = (dbCmd, row) =>
            {
                var auditRow = row as IAuditable;
                if (null != auditRow)
                {
                    auditRow.ModifyDate = DateTime.UtcNow;
                }
            };

            // Settings
            container.Register(new AppSettings());
            container.RegisterAutoWired<OrmLiteAppSettings>();
            container.Register(c => new MultiAppSettings(c.Resolve<OrmLiteAppSettings>(), c.Resolve<AppSettings>()));
            container.Resolve<OrmLiteAppSettings>().InitSchema();

            // Stores
            container.RegisterAutoWiredAs<StateStore, IStateStore>();
            container.RegisterAutoWiredAs<CounterStore, ICounterStore>();

            using (var ctx = container.Resolve<IDbConnectionFactory>().Open())
            {
                ctx.CreateTableIfNotExists<State>();
            }
        }
    }
}