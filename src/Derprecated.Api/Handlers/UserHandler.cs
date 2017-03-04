namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.OrmLite;
    using Auth0.ManagementApi;

    public class UserHandler
    {
        public ManagementApiClient Auth0 { get; set; }

        public UserAuth GetUser(string id)
        {
            id.ThrowIfNullOrEmpty();

            var req = Auth0.Users.GetAsync(id);
            req.Wait();
            return new UserAuth().PopulateWith(req.Result);
        }

        public long Count()
        {
            return 0;
            // return Db.Count<UserAuth>();
        }

        public List<UserAuth> List(int page = 0, int perPage = 25)
        {
            var req = Auth0.Users.GetAllAsync(page, perPage);
            req.Wait();
            return req.Result.ToList().ConvertAll(x => x.ConvertTo<UserAuth>());
        }

        public UserAuth Update(string id, UserAuth user)
        {
            id.ThrowIfNullOrEmpty();
            user.ThrowIfNull();

            throw new NotImplementedException();
            // var existing = UserAuthRepository.GetUserAuth(id);
            // var updates = new UserAuth().PopulateWith(existing).PopulateWithNonDefaultValues(user);
            //
            // UserAuthRepository.UpdateUserAuth(existing, updates);
            //
            // return updates;
        }

        /// <summary>
        ///     Sets the given user's permissions to the given <paramref name="permissions" />
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public List<string> SetPermissions(string userId, ICollection<string> permissions)
        {
            throw new NotImplementedException();

            // var user = GetUser(userId);
            // var current = GetPermissions(userId);
            // var remove = current.Except(permissions);

            // UserAuthRepository.UnAssignRoles(user, permissions: remove.ToArray());
            // UserAuthRepository.AssignRoles(user, permissions: permissions.ToArray());
            //
            // return permissions.ToList();
        }
    }
}
