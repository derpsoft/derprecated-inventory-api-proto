namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using Location = Models.Location;

    public class LocationService : CrudService<Location, Models.Dto.Location>
    {
        protected static ILog Log = LogManager.GetLogger(typeof(LocationService));

        public LocationService(LocationHandler db)
            :base(db)
        {
        }

        public object Any(LocationCount request)
        {
            var resp = new Dto<long>();
            var handler = new LocationHandler(Db);

            resp.Result = handler.Count();

            return resp;
        }

        //public object Get(Models.Dto.Location request)
        //{
        //    var resp = new Dto<Models.Dto.Location>();
        //    var handler = new LocationHandler(Db, CurrentSession);

        //    resp.Result = Models.Dto.Location.From(handler.Get(request.Id));

        //    return resp;
        //}

        //public object Delete(Models.Dto.Location request)
        //{
        //    var resp = new Dto<Models.Dto.Location>();
        //    var handler = new LocationHandler(Db, CurrentSession);

        //    resp.Result = Models.Dto.Location.From(handler.Delete(request.Id));

        //    return resp;
        //}

        public object Any(Models.Dto.Location request)
        {
            var resp = new Dto<Models.Dto.Location>();
            var handler = new LocationHandler(Db);
            var newLocation = handler.Save(new Location().PopulateWith(request));

            resp.Result = newLocation.ConvertTo<Models.Dto.Location>();

            return resp;
        }

        public object Get(Locations request)
        {
            var resp = new Dto<List<Models.Dto.Location>>();
            var handler = new LocationHandler(Db);

            resp.Result = handler.List(request.Skip, request.Take)
                .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());

            return resp;
        }

        public object Any(LocationTypeahead request)
        {
            var resp = new Dto<List<Models.Dto.Location>>();
            var locationHandler = new LocationHandler(Db);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = locationHandler.List(0, int.MaxValue)
                    .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());
            else
                resp.Result = searchHandler.LocationTypeahead(request.Query)
                    .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());

            return resp;
        }
    }
}
