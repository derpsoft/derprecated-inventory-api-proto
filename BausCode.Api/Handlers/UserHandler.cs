namespace BausCode.Api.Handlers
{
    using System.Data;
    using Models;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.OrmLite;

    public class UserHandler
    {
        public UserHandler(IDbConnection db, IUserAuthRepository userAuthRepository, UserSession user)
        {
            Db = db;
            User = user;
            UserAuthRepository = userAuthRepository;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        private IUserAuthRepository UserAuthRepository { get; }

        public UserAuth GetUser(int id)
        {
            id.ThrowIfLessThan(1);
            return UserAuthRepository.GetUserAuth(id) as UserAuth;
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
    }
}
