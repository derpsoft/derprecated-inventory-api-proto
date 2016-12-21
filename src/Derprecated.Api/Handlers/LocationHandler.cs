namespace Derprecated.Api.Handlers
{
    using System.Data;
    using Api.Models;
    using ServiceStack.OrmLite;

    public class LocationHandler
    {
        public LocationHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; set; }

        public Location GetLocation(int id)
        {
            id.ThrowIfLessThan(1);
            return Db.SingleById<Location>(id);
        }
    }
}
