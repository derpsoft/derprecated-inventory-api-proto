using System;

namespace BausCode.Api.Jobs
{
    public interface ICounterStore : IDisposable
    {
        long Get(Counter counter);
        long Increment(Counter counter);
    }
}