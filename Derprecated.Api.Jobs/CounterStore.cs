namespace Derprecated.Api.Jobs
{
    using System;
    using ServiceStack;
    using ServiceStack.Caching;

    public class CounterStore : ICounterStore
    {
        public ICacheClient Client { get; set; }
        private bool IsDisposed { get; set; }

        /// <summary>
        ///     Decrement the value of a counter by a specific amount.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        private long IncrementBy(string key, uint increment)
        {
            return Client.Increment(key, increment);
        }

        /// <summary>
        ///     Decrement the value of a counter by a specific amount.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="decrement"></param>
        /// <returns></returns>
        private long DecrementBy(string key, uint decrement)
        {
            return Client.Decrement(key, decrement);
        }

        /// <summary>
        ///     Get the current value of a counter.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private long Get(string key)
        {
            return Client.Get<long>(key);
        }

        /// <summary>
        ///     Set a counter to a specific value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, long value)
        {
            return Client.Set(key, value);
        }

        /// <summary>
        ///     Set a counter's value back to 0.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool Reset(string key)
        {
            return Set(key, 0);
        }

        /// <summary>
        ///     Increment the given counter.
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        public long Increment(Counter counter)
        {
            counter.ThrowIfNull();

            return IncrementBy(counter.CreateUrn(), counter.DefaultIncrement);
        }

        public long Get(Counter counter)
        {
            counter.ThrowIfNull();

            return Get(counter.CreateUrn());
        }

        #region IDisposable

        public void Dispose()
        {
            if (!IsDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                IsDisposed = true;
            }
        }

        private void Dispose(bool managed)
        {
            if (managed)
            {
                if (null != Client)
                {
                    Client.Dispose();
                }
            }
        }

        #endregion
    }
}
