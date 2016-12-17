namespace Derprecated.Api.Services
{
    using Handlers;
    using Models;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Logging;
    using ServiceStack.OrmLite;

    public class UserService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (UserService));

        public object Any(GetProfile request)
        {
            var res = new ProfileResponse();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);

            res.Profile = Profile.From(CurrentSession);

            return new HttpResult(res);
        }

        private object UpdateUserField<T>(IUpdatableField<T> request)
        {
            var resp = new ProductResponse();

            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);
            handler.Update(request.Id, request);

            return resp;
        }

        public object Any(UpdateUser request)
        {
            var resp = new GetUserResponse();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);

            resp.User = User.From(handler.Update(request.Id, new UserAuth().PopulateWithNonDefaultValues(request)));

            return resp;
        }

        public object Any(UpdateUserFirstName request)
        {
            return UpdateUserField(request);
        }

        public object Any(UpdateUserLastName request)
        {
            return UpdateUserField(request);
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

            res.Profile = Profile.From(CurrentSession);
            return res;
        }

        public object Any(Register request)
        {
            UserAuthRepository.CreateUserAuth(new UserAuth().PopulateWith(request), request.Password);

            using (var service = ResolveService<AuthenticateService>())
            {
                Request.RemoveSession();
                return service.Authenticate(new Authenticate
                                            {
                                                provider = AuthenticateService.CredentialsProvider,
                                                UserName = request.Email,
                                                Password = request.Password
                                            });
            }
        }

        public object Any(GetUsers request)
        {
            var response = new GetUsersResponse();

            response.Users = Db.Select(Db.From<UserAuth>()
                                         .Skip(request.Skip.GetValueOrDefault(0))
                                         .Take(request.Take.GetValueOrDefault(25))
                ).Map(User.From);
            response.Count = Db.Count<UserAuth>();

            return response;
        }

        public object Any(GetUser request)
        {
            var response = new GetUserResponse();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);
            var user = handler.GetUser(request.Id);

            if (null != user)
                response.User = User.From(user);

            return response;
        }

        public object Any(UserRoles request)
        {
            var resp = new RolesResponse();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);

            resp.Roles = handler.GetRoles();

            return resp;
        }

        public object Any(UserPermissions request)
        {
            var resp = new PermissionsResponse();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);

            resp.Permissions = handler.GetPermissions();

            return resp;
        }
    }
}
