using System;
using System.Configuration;
using BausCode.Api.Jobs.Authentication;
using Funq;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.Redis;
using Configuration = BausCode.Api.Models.Configuration;

namespace BausCode.Api.Jobs.Spout
{
    public class Program : ProgramBase
    {
        protected override void Configure(Container container)
        {
            base.Configure(container);

            var settings = container.Resolve<MultiAppSettings>();


            container.Register<IRedisClientsManager>(
                new RedisManagerPool(ConfigurationManager.ConnectionStrings["AzureRedis"].ConnectionString));


            var configuration = new Configuration
            {
                RestBaseUri = settings.Get("bc.twitter.baseUri"),
                StreamBaseUri = settings.Get("bc.twitter.streamBaseUri"),
                ConsumerKey = settings.Get("oauth.twitter.ConsumerKey"),
                ConsumerSecret = settings.Get("oauth.twitter.ConsumerSecret"),
                AccessToken = settings.Get("bc.jobs.opm.spout.accessToken"),
                AccessTokenSecret = settings.Get("bc.jobs.opm.spout.accessTokenSecret"),
                QueueName = settings.Get("bc.jobs.opm.queueName"),
                BatchSize = settings.Get("bc.jobs.opm.spout.batchSize", 10),
                ShutdownFile = Environment.GetEnvironmentVariable("WEBJOBS_SHUTDOWN_FILE"),
                KeywordReloadMinutes = settings.Get("bc.jobs.opm.spout.reloadMinutes", 10)
            };

            container.Register(configuration);
            container.Register(c => new BearerOAuth(configuration.ConsumerKey, configuration.ConsumerSecret));
            container.Register(
                c =>
                    new ThreeLeggedOAuth(configuration.ConsumerKey, configuration.ConsumerSecret,
                        configuration.AccessToken, configuration.AccessTokenSecret, c.Resolve<IDbConnectionFactory>()));

            container.RegisterAs<Job, IJob>();
        }

        public static void Main(string[] args)
        {
            var app = new Program();
            app.Init();
        }
    }
}