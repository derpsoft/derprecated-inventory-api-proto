namespace Derprecated.Api.Services
{
    using Handlers;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;

    public class ProfileService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(ProfileService));

        public object Get(Profile request)
        {
            var res = new Dto<Profile>();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);

            res.Result = Profile.From(CurrentSession);

            return new HttpResult(res);
        }

        public object Any(Profile request)
        {
            var res = new Dto<Profile>();
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

                //var avatarUrl = ImageHandler.SaveImage(Request.Files.First());

                //avatar.Url = avatarUrl.ToString();
                //Db.Save(avatar);
                //CurrentSession.Avatar = avatar;
                //Redis.Set($"user:avatar:{user.Id}", avatar);
            }

            Request.SaveSession(CurrentSession);

            res.Result = Profile.From(CurrentSession);
            return res;
        }

    }
}
