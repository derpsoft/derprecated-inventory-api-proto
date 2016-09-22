using System.Linq;
using BausCode.Api.Models;
using BausCode.Api.Models.Routing;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.OrmLite;

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

        public object Any(UpdateProfile request)
        {
            var res = new ProfileResponse();
            var userId = CurrentSession.UserAuthId;
            var user = UserAuthRepository.GetUserAuth(userId);

            if (!request.DisplayName.IsNullOrEmpty() && !user.DisplayName.Equals(request.DisplayName))
            {
                user.DisplayName = request.DisplayName;
                CurrentSession.DisplayName = user.DisplayName;
            }

            if (!request.PhoneNumber.IsNullOrEmpty())
            {
                user.PhoneNumber = request.PhoneNumber;
                CurrentSession.PhoneNumber = user.PhoneNumber;
            }

            UserAuthRepository.UpdateUserAuth(user, user);
            //Redis.Set($"user:displayName:{user.Id}", user.DisplayName);

            if (Request.Files.Length > 0)
            {
                //var avatar = Db.Single(Db.From<Avatar>()
                //    .Where(x => x.UserAuthId == userId)
                //    .Limit(1)) ?? new Avatar { UserAuthId = userId };

                //var avatarUrl = ImageHandler.SaveAvatarImage(Request.Files.First());

                //avatar.Url = avatarUrl.ToString();
                //Db.Save(avatar);
                //CurrentSession.Avatar = avatar;
                //Redis.Set($"user:avatar:{user.Id}", avatar);
            }

            Request.SaveSession(CurrentSession);

            res.Profile = Models.Dto.Profile.From(CurrentSession);
            return res;
        }

    }
}