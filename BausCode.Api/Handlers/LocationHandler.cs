using System.Data;
using BausCode.Api.Models;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    public class LocationHandler
    {
        private IDbConnection Db { get; set; }
        private UserSession User { get; set; }

        public LocationHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        public Location GetLocation(int id)
        {
            id.ThrowIfLessThan(1);
            return Db.SingleById<Location>(id);
        }
    }
}