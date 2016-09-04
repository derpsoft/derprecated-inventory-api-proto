using BausCode.Api.Models;
using BausCode.Api.Models.Routing;
using ServiceStack;
using ServiceStack.Logging;

namespace BausCode.Api.Services
{
    public class UserService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(UserService));

        public object Any(GetUser request)
        {
            var response = new GetUserResponse();

            response.User = Models.Dto.UserSession.From(SessionAs<UserSession>());

            return new HttpResult(response);
        }
    }
}