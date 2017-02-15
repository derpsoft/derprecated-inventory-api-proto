namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Logging;
    using ServiceStack.OrmLite;

    public class UserService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(UserService));

        public object Any(UserCount request)
        {
            var resp = new Dto<long>();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);

            resp.Result = handler.Count();

            return resp;
        }

        public object Get(User request)
        {
            var response = new Dto<User>();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);
            var user = handler.GetUser(request.Id);

            if (null != user)
            {
                response.Result = User.From(user);
                response.Result.Permissions = handler.GetPermissions(user.Id);
            }

            return response;
        }

        public object Any(User request)
        {
            var resp = new Dto<User>();
            var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);

            var update = handler.Update(request.Id, new UserAuth().PopulateWithNonDefaultValues(request));
            update.Permissions = request.Permissions.IsNullOrEmpty()
                ? handler.GetPermissions(request.Id)
                : handler.SetPermissions(request.Id, request.Permissions);

            resp.Result = User.From(update);

            return resp;
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

        public object Any(Users request)
        {
            var response = new Dto<List<User>>();

            response.Result = Db.Select(Db.From<UserAuth>()
                                          .Skip(request.Skip.GetValueOrDefault(0))
                                          .Take(request.Take.GetValueOrDefault(25))
                                ).Map(User.From);

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

        public object Any(UserTypeahead request)
        {
            var resp = new Dto<List<User>>();
            var userHandler = new UserHandler(Db, UserAuthRepository, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = userHandler.List(0, int.MaxValue).Map(User.From);
            else
                resp.Result = searchHandler.UserTypeahead(request.Query).Map(User.From);

            return resp;
        }
    }
}
