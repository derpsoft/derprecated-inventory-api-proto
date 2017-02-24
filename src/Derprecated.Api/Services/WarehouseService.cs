namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using Warehouse = Models.Warehouse;

    public static class WarehouseServices
    {
        private static ILog Log = LogManager.GetLogger(typeof(WarehouseService));

        // ReSharper disable once MemberCanBePrivate.Global
        public class WarehouseService : CrudService<Warehouse, Models.Dto.Warehouse>
        {
            public WarehouseService(IHandler<Warehouse> handler) : base(handler)
            {
            }

            public object Get(WarehouseCount request)
            {
                var resp = new Dto<long>();

                resp.Result = Handler.Count();

                return resp;
            }

            public object Get(Warehouses request)
            {
                var resp = new Dto<List<Models.Dto.Warehouse>>();

                resp.Result = Handler.List(request.Skip, request.Take).Map(Models.Dto.Warehouse.From);

                return resp;
            }

            public object Any(WarehouseTypeahead request)
            {
                var resp = new Dto<List<Models.Dto.Warehouse>>();

                if (request.Query.IsNullOrEmpty())
                    resp.Result = Handler.List(0, int.MaxValue).Map(Models.Dto.Warehouse.From);
                else
                    resp.Result = Handler.Typeahead(request.Query, request.IncludeDeleted)
                                         .Map(Models.Dto.Warehouse.From);
                return resp;
            }
        }
    }
}
