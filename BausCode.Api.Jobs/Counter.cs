using System;
using ServiceStack;

namespace BausCode.Api.Jobs
{
    public abstract class Counter
    {
        protected Counter(ICounterStore counterStore, string name, TimeSpan resetInterval, uint defaultIncrement = 1,
            uint defaultDecrement = 1, long max = long.MaxValue)
        {
            counterStore.ThrowIfNull();
            name.ThrowIfNullOrEmpty();
            resetInterval.ThrowIfNull();

            CounterStore = counterStore;

            Name = name;
            DefaultIncrement = defaultIncrement;
            DefaultDecrement = defaultDecrement;
            ResetInterval = resetInterval;
            Max = max;
        }

        private ICounterStore CounterStore { get; set; }

        /// <summary>
        ///     The name or Id of the counter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The default value to increment by.
        /// </summary>
        public uint DefaultIncrement { get; set; }

        /// <summary>
        ///     The default value to decrement by.
        /// </summary>
        public uint DefaultDecrement { get; set; }

        /// <summary>
        ///     The counter reset interval.
        /// </summary>
        public TimeSpan ResetInterval { get; set; }

        /// <summary>
        ///     The maximum value for this counter.
        /// </summary>
        public long Max { get; set; }

        public bool IsMax()
        {
            return CounterStore.Get(this) >= Max;
        }

        public void Increment()
        {
            CounterStore.Increment(this);
        }
    }
}