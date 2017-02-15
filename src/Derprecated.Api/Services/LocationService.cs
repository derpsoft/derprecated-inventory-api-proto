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

            resp.Result = Handler.Count();

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
            var newLocation = Handler.Save(new Location().PopulateWith(request));

            resp.Result = newLocation.ConvertTo<Models.Dto.Location>();

            return resp;
        }

        public object Get(Locations request)
        {
            var resp = new Dto<List<Models.Dto.Location>>();

            resp.Result = Handler.List(request.Skip, request.Take)
                .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());

            return resp;
        }

        public object Any(LocationTypeahead request)
        {
            var resp = new Dto<List<Models.Dto.Location>>();
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = Handler.List(0, int.MaxValue)
                    .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());
            else
                resp.Result = searchHandler.LocationTypeahead(request.Query)
                    .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());

            return resp;
        }
    }
}
