using System;
using System.Data;
using System.Linq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace BausCode.Api.Jobs
{
    public class StateStore : IStateStore
    {
        private bool isDisposed;

        public StateStore(IDbConnectionFactory contextFactory)
            : this(contextFactory.Open())
        {
        }

        public StateStore(IDbConnection context)
        {
            Context = context;
        }

        private IDbConnection Context { get; set; }

        public T Get<T>(string key)
        {
            key.ThrowIfNullOrEmpty("key");

            var value = Get(key);
            if (null == value)
            {
                return default(T);
            }
            return value.Value.FromJson<T>();
        }

        public void Set<T>(string key, T value)
        {
            key.ThrowIfNullOrEmpty("key");
            value.ThrowIfNull("value");

            var set = Get(key) ?? new State {Key = key.ToLowerInvariant()};
            set.Value = value.ToJson();
            Context.Save(set);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                Dispose(true);
                isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        private State Get(string key)
        {
            key.ThrowIfNullOrEmpty("key");

            return Context.Where<State>(new {Key = key.ToLowerInvariant()}).FirstOrDefault();
        }

        private void Dispose(bool managed)
        {
            if (managed)
            {
                if (null != Context)
                {
                    Context.Dispose();
                }
            }
        }
    }
}