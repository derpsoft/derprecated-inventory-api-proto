namespace Derprecated.Api.Services
{
    using Models;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Auth;

    public abstract class BaseService : Service
    {
        public ApplicationConfiguration Configuration { get; set; }
        internal IAuthSession CurrentSession => GetSession();
        public IUserAuthRepository UserAuthRepository { get; set; }
    }
}
