namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using Handlers;
    using Models;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Logging;

    public class UserService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(UserService));

        public UserHandler Handler { get; set; }

        public object Any(UserCount request)
        {
            var resp = new Dto<long>();

            resp.Result = Handler.Count();

            return resp;
        }

        public object Get(User request)
        {
            var response = new Dto<User>();

            response.Result = Handler.GetUser(request.Id).ToDto();

            return response;
        }

        public object Any(User request)
        {
            var resp = new Dto<User>();
            // var handler = new UserHandler(Db, UserAuthRepository, CurrentSession);

            // update.Permissions = request.Permissions.IsNullOrEmpty()
            //     ? handler.GetPermissions(request.Id)
            //     : handler.SetPermissions(request.Id, request.Permissions);

            resp.Result = Handler.Update(request.Id, request.FromDto()).ToDto();

            return resp;
        }

        public object Any(Users request)
        {
            var response = new Dto<List<User>>();

            response.Result = Handler.List(request.Skip.GetValueOrDefault(0),
                                         request.Take.GetValueOrDefault(50))
                                     .ConvertAll(x => x.ToDto());

            return response;
        }

        public object Any(UserRoles request)
        {
            var resp = new RolesResponse();

            // resp.Roles = Handler.GetRoles();

            return resp;
        }

        public object Any(UserPermissions request)
        {
            var resp = new PermissionsResponse();

            // resp.Permissions = Handler.GetPermissions();

            return resp;
        }

        public object Any(UserTypeahead request)
        {
            throw new NotImplementedException();
            // var resp = new Dto<List<User>>();
            // var userHandler = new UserHandler(Db, UserAuthRepository, CurrentSession);
            // var searchHandler = new SearchHandler(Db, CurrentSession);
            //
            // if (request.Query.IsNullOrEmpty())
            //     resp.Result = userHandler.List(0, int.MaxValue).Map(User.From);
            // else
            //     resp.Result = searchHandler.UserTypeahead(request.Query).Map(User.From);
            //
            // return resp;
        }
    }
}
