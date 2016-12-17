namespace Derprecated.Api.Jobs.Authentication
{
    using ServiceStack;

    public interface IUnauthenticatedApiProxy
    {
        TOut Authenticate<TIn, TOut>(TIn request)
            where TIn : IHasCredentials, IReturn<TOut>;
    }
}
