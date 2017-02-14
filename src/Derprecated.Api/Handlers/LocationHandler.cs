namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using ServiceStack;
    using ServiceStack.OrmLite;

    public class LocationHandler : CrudHandler<Location>
    {
        public LocationHandler(IDbConnection db)
            : base(db)
        {
        }

        public List<Location> List(int skip = 0, int take = 25)
        {
            return Db.Select(
                Db.From<Location>()
                    .Where(x => !x.IsDeleted)
                    .Skip(skip)
                    .Take(take)
            );
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
