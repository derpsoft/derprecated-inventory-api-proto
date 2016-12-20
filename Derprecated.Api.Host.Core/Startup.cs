namespace Derprecated.Api.Host.Core
{
    using Configuration.Core;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Models.Configuration;
    using Services;
    using ServiceStack;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddEnvironmentVariables();
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

            Configuration = builder.Build();

            Licensing.RegisterLicense(FindLicense(Configuration));
        }

        private IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<ApplicationConfiguration>(Configuration);
        }

        private string FindLicense(IConfigurationRoot parentConfig)
        {
            return parentConfig.GetSection("serviceStack")["license"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            //loggerFactory.AddNLog();
            //env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new Application("Web",
                typeof (BaseService).GetAssembly()));

            app.Run(async context => { context.Response.Redirect("/metadata"); });
        }
    }
}
