namespace BausCode.Api.Jobs.Models
{
    using System;

    public class SearchTweetsRateLimitCounter : Counter
    {
        private const string NAME = "SearchTweetsRateLimitCounter";
        private const uint INCREMENT = 1;
        private const uint DECREMENT = 1;
        private const uint MAX = 300;
        // https://dev.twitter.com/rest/public/rate-limits indicates that this should be 450, but science has shown the number to be closer to 300

        private static readonly TimeSpan RESET_INTERVAL = TimeSpan.FromMinutes(15);

        public SearchTweetsRateLimitCounter(ICounterStore counterStore)
            : base(
                counterStore, NAME, defaultIncrement: INCREMENT, defaultDecrement: DECREMENT, max: MAX,
                resetInterval: RESET_INTERVAL)
        {
        }
    }
}
