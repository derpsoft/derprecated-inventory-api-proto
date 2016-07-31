using BausCode.Api.Models;
using ServiceStack;
using ServiceStack.Auth;

namespace BausCode.Api.Services
{
    public abstract class BaseService : Service
    {
        public Configuration Configuration { get; set; }

        private UserSession userSession;
        internal UserSession CurrentUser => userSession ?? (userSession = SessionAs<UserSession>());
    }
}