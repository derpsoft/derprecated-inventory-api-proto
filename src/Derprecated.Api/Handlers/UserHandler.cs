namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.OrmLite;

    public class UserHandler
    {
        public UserHandler(IDbConnection db, IUserAuthRepository userAuthRepository, IAuthSession user)
        {
            Db = db;
            UserAuthRepository = userAuthRepository;
            User = user;
        }

        private IDbConnection Db { get; }

        private IAuthSession User { get; }

        private IUserAuthRepository UserAuthRepository { get; }

        public UserAuth GetUser(int id)
        {
            id.ThrowIfLessThan(1);
            return UserAuthRepository.GetUserAuth(id) as UserAuth;
        }

        public long Count()
        {
            return Db.Count<UserAuth>();
        }

        public List<UserAuth> List(int skip = 0, int take = 25)
        {
            return Db.Select(
                Db.From<UserAuth>()
                  .Skip(skip)
                  .Take(take)
            );
        }

        public UserAuth Update(int id, UserAuth user)
        {
            id.ThrowIfLessThan(1);
            user.ThrowIfNull();

            var existing = UserAuthRepository.GetUserAuth(id);
            var updates = new UserAuth().PopulateWith(existing).PopulateWithNonDefaultValues(user);

            UserAuthRepository.UpdateUserAuth(existing, updates);

            return updates;
        }

        public UserAuth Update<T>(int id, IUpdatableField<T> update)
        {
            update.ThrowIfNull();
            var user = GetUser(id);
            user.SetProperty(update.FieldName, update.Value);
            Db.UpdateOnly(user, new[] {update.FieldName}, p => p.Id == user.Id);
            return user;
        }

        public List<string> GetRoles(int id)
        {
            var user = GetUser(id);
            return UserAuthRepository.GetRoles(user).ToList();
        }

        public List<string> GetRoles()
        {
            User.ThrowIfNull();
            return GetRoles(User.UserAuthId.ToInt());
        }

        public List<string> GetPermissions()
        {
            User.ThrowIfNull();
            return GetPermissions(User.UserAuthId.ToInt());
        }

        public List<string> GetPermissions(int id)
        {
            var user = GetUser(id);
            return UserAuthRepository.GetPermissions(user).ToList();
        }

        /// <summary>
        ///     Sets the given user's permissions to the given <paramref name="permissions" />
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public List<string> SetPermissions(int userId, ICollection<string> permissions)
        {
            var user = GetUser(userId);
            var current = GetPermissions(userId);
            var remove = current.Except(permissions);

            UserAuthRepository.UnAssignRoles(user, permissions: remove.ToArray());
            UserAuthRepository.AssignRoles(user, permissions: permissions.ToArray());

            return permissions.ToList();
        }
    }
}
