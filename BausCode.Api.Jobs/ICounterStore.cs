namespace BausCode.Api.Jobs
{
    using System;

    public interface ICounterStore : IDisposable
    {
        long Get(Counter counter);
        long Increment(Counter counter);
    }
}
