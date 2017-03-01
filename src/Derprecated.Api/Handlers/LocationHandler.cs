namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using Models;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    public class LocationHandler : CrudHandler<Location>
    {
        public LocationHandler(IDbConnectionFactory db)
            : base(db)
        {
        }

        public override List<Location> Typeahead(string query, bool includeDeleted = false)
        {
            return Db.Select(
                Db.From<Location>()
                  .Where(x => x.Name.Contains(query))
                  .SelectDistinct()
            );
        }
    }
}
