namespace Derprecated.Api.Jobs
{
    using System;

    public interface IStateStore : IDisposable
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}
