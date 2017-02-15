namespace Derprecated.Api.Handlers
{
    using Models;
    using ServiceStack.Data;

    public class LocationHandler : CrudHandler<Location>
    {
        public LocationHandler(IDbConnectionFactory db)
            : base(db)
        {
        }
    }
}
