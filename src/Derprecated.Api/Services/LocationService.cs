namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Logging;

    public class LocationService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (LocationService));

        public object Any(LocationCount request)
        {
            var resp = new CountResponse();
            var handler = new LocationHandler(Db, CurrentSession);

            resp.Count = handler.Count();

            return resp;
        }

        public object Get(Location request)
        {
            var resp = new Dto<Location>();
            var handler = new LocationHandler(Db, CurrentSession);

            resp.Result = Location.From(handler.Get(request.Id));

            return resp;
        }

        public object Delete(Location request)
        {
            throw new NotImplementedException();
            var resp = new Dto<bool>();
            var handler = new LocationHandler(Db, CurrentSession);

            return resp;
        }

        public object Any(Location request)
        {
            var resp = new Dto<Location>();
            var handler = new LocationHandler(Db, CurrentSession);
            var newLocation = handler.Save(new Models.Location().PopulateWith(request));

            resp.Result = Location.From(newLocation);

            return resp;
        }

        public object Get(Locations request)
        {
            var resp = new Dto<List<Location>>();
            var handler = new LocationHandler(Db, CurrentSession);

            resp.Result = handler.List(request.Skip, request.Take).Map(Location.From);

            return resp;
        }
    }
}
