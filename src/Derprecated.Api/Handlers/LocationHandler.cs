namespace Derprecated.Api.Handlers
{
    using System;
    using System.Data;
    using Models;
    using ServiceStack;
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

        public Location Get(int id)
        {
            id.ThrowIfLessThan(1);
            return Db.SingleById<Location>(id);
        }

        public Location Save(Location location)
        {
            location.ThrowIfNull();
            if (location.Id >= 1)
            {
                var existing = Get(location.Id);
                if (default(Location) == existing)
                    throw new ArgumentException("invalid Id for existing location", nameof(location));

                location = existing.PopulateWith(location);
            }
            Db.Save(location);

            return location;
        }

        public long Count()
        {
            return Db.Count<Location>();
        }
    }
}
