namespace Derprecated.Api.Services
{
    using Api.Models;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Configuration;

    public abstract class BaseService : Service
    {
        private UserSession userSession;
        public ApplicationConfiguration Configuration { get; set; }
        internal UserSession CurrentSession => userSession ?? (userSession = SessionAs<UserSession>());
        public IUserAuthRepository UserAuthRepository { get; set; }
    }
}
