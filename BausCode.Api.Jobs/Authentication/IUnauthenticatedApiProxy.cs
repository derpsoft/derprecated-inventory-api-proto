using ServiceStack;

namespace BausCode.Api.Jobs.Authentication
{
    public interface IUnauthenticatedApiProxy
    {
        TOut Authenticate<TIn, TOut>(TIn request)
            where TIn : IHasCredentials, IReturn<TOut>;
    }
}