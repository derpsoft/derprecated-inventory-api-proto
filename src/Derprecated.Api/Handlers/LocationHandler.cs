namespace Derprecated.Api.Handlers
{
    using System.Data;
    using Models;

    public class LocationHandler : CrudHandler<Location>
    {
        public LocationHandler(IDbConnection db)
            : base(db)
        {
        }
    }
}
