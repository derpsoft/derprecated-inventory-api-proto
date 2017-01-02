namespace Derprecated.Api.Services
{
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Logging;

    public class LocationService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (LocationService));

        public object Any(CountLocations request)
        {
            var resp = new CountResponse();
            var handler = new LocationHandler(Db, CurrentSession);

            resp.Count = handler.Count();

            return resp;
        }

        public object Any(GetLocation request)
        {
            var resp = new LocationResponse();
            var handler = new LocationHandler(Db, CurrentSession);

            resp.Location = Location.From(handler.Get(request.Id));

            return resp;
        }

        public object Any(GetLocations request)
        {
            var resp = new LocationsResponse();
            var handler = new LocationHandler(Db, CurrentSession);

            resp.Locations = handler.List(request.Skip, request.Take).Map(Location.From);

            return resp;
        }

        public object Any(CreateLocation request)
        {
            var resp = new LocationResponse();
            var handler = new LocationHandler(Db, CurrentSession);
            var newLocation = handler.Save(new Models.Location().PopulateWith(request.Location));

            resp.Location = Location.From(newLocation);

            return resp;
        }

        public object Any(UpdateLocation request)
        {
            var resp = new LocationResponse();
            var handler = new LocationHandler(Db, CurrentSession);
            var update = handler.Save(new Models.Location
                                      {
                                          Id = request.Id
                                      }.PopulateWith(request.Location));

            resp.Location = Location.From(update);


            return resp;
        }
    }
}
