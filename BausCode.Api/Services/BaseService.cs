using BausCode.Api.Models;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;

namespace BausCode.Api.Services
{
    public abstract class BaseService : Service
    {
        public Configuration Configuration { get; set; }
        public AppSettings AppSettings { get; set; }
        public IUserAuthRepository UserAuthRepository { get; set; }

        private UserSession userSession;
        internal UserSession CurrentSession => userSession ?? (userSession = SessionAs<UserSession>());
    }
}