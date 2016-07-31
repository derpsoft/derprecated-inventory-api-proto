using BausCode.Api.Models;
using BausCode.Api.Models.Routing;
using ServiceStack;

namespace BausCode.Api.Services
{
    public class UserService : BaseService
    {
        public object Any(GetUser request)
        {
            var response = new GetUserResponse();

            response.User = Models.Dto.UserSession.From(SessionAs<UserSession>());

            return new HttpResult(response);
        }
    }
}