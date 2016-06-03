using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BausCode.Api.Jobs.Authentication;
using BausCode.Api.Jobs.Models;
using BausCode.Api.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Redis;

// ReSharper disable MemberCanBePrivate.Global

namespace BausCode.Api.Jobs.Spout
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Job : IJob
    {
        public ThreeLeggedOAuth Auth { get; set; }
        public Configuration Configuration { get; set; }
        public IRedisClientsManager RedisManager { get; set; }
        public IDbConnectionFactory DbConnectionFactory { get; set; }

        public virtual void Run()
        {
            var running = true;
            var timer = new Stopwatch();
            var reloadInterval = TimeSpan.FromMinutes(Configuration.KeywordReloadMinutes);
            var streamingApiProxy = new ApiProxy(Configuration.StreamBaseUri);
            var request = new StatusesFilter();
            var directory = Path.GetDirectoryName(Configuration.ShutdownFile);

            if (!Configuration.ShutdownFile.IsNullOrEmpty() && null != directory)
            {
                var fileSystemWatcher = new FileSystemWatcher(directory);

                fileSystemWatcher.Created +=
                    (sender, args) => running = !args.FullPath.Contains(Configuration.ShutdownFile);
                fileSystemWatcher.Changed +=
                    (sender, args) => running = !args.FullPath.Contains(Configuration.ShutdownFile);
                fileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName |
                                                 NotifyFilters.LastWrite;
                fileSystemWatcher.IncludeSubdirectories = false;
                fileSystemWatcher.EnableRaisingEvents = true;
            }

            request.Delimited = "length";

            while (running)
            {
                timer.Restart();
                using (var redis = RedisManager.GetClient())
                {
                    using (var ctx = DbConnectionFactory.Open())
                    {
                        var q = GetQuery(ctx);
                        if (!q.EqualsIgnoreCase(request.Track))
                        {
                            var reset = new {k = "RESET", t = q}.ToJson();
                            redis.AddItemToList(Configuration.QueueName, reset);
                            Console.WriteLine("{0} Reset [{1}]", timer.Elapsed, reset);
                        }

                        Console.WriteLine("{0} Track [{1}]", timer.Elapsed, q);
                        request.Track = q;
                    }

                    if (string.IsNullOrEmpty(request.Track))
                    {
                        Console.WriteLine("{0} Halt [nothing to track]", timer.Elapsed);
                        break;
                    }

                    using (var stream = streamingApiProxy.StreamStatusesFilter(Auth, request))
                    using (var sb = new StreamReader(stream))
                    {
                        var batch = new List<string>(Configuration.BatchSize*2);

                        while (running)
                        {
                            string line;
                            do
                            {
                                line = sb.ReadLine();
                                // ReSharper disable once PossibleNullReferenceException
                            } while (line.Length < 1);

                            var chunkLen = line.ToInt();
                            var buf = new char[chunkLen];

                            sb.ReadBlock(buf, 0, chunkLen);
                            batch.Add(new string(buf));

                            // ReSharper disable once InvertIf
                            if (batch.Count >= Configuration.BatchSize || timer.Elapsed > reloadInterval)
                            {
                                redis.AddRangeToList(Configuration.QueueName, batch);
                                batch.Clear();

                                if (timer.Elapsed > reloadInterval)
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public virtual string GetQuery(IDbConnection ctx)
        {
            return ctx.Select<Keyword>()
                .OrderByDescending(kw => kw.CreateDate)
                .Select(kw => kw.Value)
                .FirstOrDefault();
        }
    }
}