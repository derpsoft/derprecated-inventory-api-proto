using System;

namespace BausCode.Api.Jobs
{
    public interface IStateStore : IDisposable
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}