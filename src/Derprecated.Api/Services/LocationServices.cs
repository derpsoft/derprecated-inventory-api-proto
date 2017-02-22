namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using Location = Models.Location;

    public static class LocationServices
    {
        private static ILog Log = LogManager.GetLogger(typeof(LocationServices));

        public class LocationCrudService : CrudService<Location, Models.Dto.Location>
        {
            public LocationCrudService(IHandler<Location> handler)
                : base(handler)
            {
            }

            public object Any(LocationTypeahead request)
            {
                var resp = new Dto<List<Models.Dto.Location>>();
                var searchHandler = new SearchHandler(Db, CurrentSession);

                if (request.Query.IsNullOrEmpty())
                    resp.Result = Handler.List(0, int.MaxValue)
                                         .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());
                else
                    resp.Result = Handler.Typeahead(request.Query, request.IncludeDeleted)
                                               .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());
                return resp;
            }

            public object Get(LocationCount request)
            {
                var resp = new Dto<long>();
                resp.Result = Handler.Count();
                return resp;
            }

            public object Get(Locations request)
            {
                var resp = new Dto<List<Models.Dto.Location>>();
                resp.Result = Handler.List(request.Skip, request.Take)
                                     .ConvertAll(x => x.ConvertTo<Models.Dto.Location>());
                return resp;
            }
        }
    }
}
