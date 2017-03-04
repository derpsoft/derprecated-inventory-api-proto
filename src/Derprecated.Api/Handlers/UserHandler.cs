﻿namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Auth0.Core;
    using ServiceStack;
    using ServiceStack.Auth;

    public class UserHandler
    {
        public Auth0Handler Auth0Handler { get; set; }

        public UserAuth GetUser(string id)
        {
            id.ThrowIfNullOrEmpty();

            return Auth0Handler.GetUser(id).ConvertTo<UserAuth>();
        }

        public long Count()
        {
            return 0;
            // return Db.Count<UserAuth>();
        }

        public List<User> List(int page = 0, int perPage = 25)
        {
            return Auth0Handler.GetAllUsers(page, perPage).ToList();
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
