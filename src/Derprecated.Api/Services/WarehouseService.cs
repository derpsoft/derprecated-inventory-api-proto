namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using Handlers;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;

    public class WarehouseService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(WarehouseService));

        public WarehouseHandler Handler { get; set; }

        public object Any(WarehouseCount request)
        {
            var resp = new Dto<long>();

            resp.Result = Handler.Count();

            return resp;
        }

        public object Get(Warehouse request)
        {
            var resp = new Dto<Warehouse>();

            resp.Result = Warehouse.From(Handler.Get(request.Id));

            return resp;
        }

        public object Delete(Warehouse request)
        {
            var resp = new Dto<Warehouse>();

            resp.Result = Warehouse.From(Handler.Delete(request.Id));

            return resp;
        }

        public object Any(Warehouse request)
        {
            var resp = new Dto<Warehouse>();
            var warehouse = new Models.Warehouse().PopulateWith(request);

            resp.Result = Warehouse.From(Handler.Save(warehouse));

            return resp;
        }

        public object Any(Warehouses request)
        {
            var resp = new Dto<List<Warehouse>>();

            resp.Result = Handler.List(request.Skip, request.Take).Map(Warehouse.From);

            return resp;
        }

        public object Any(WarehouseTypeahead request)
        {
            var resp = new Dto<List<Warehouse>>();

            if (request.Query.IsNullOrEmpty())
                resp.Result = Handler.List(0, int.MaxValue).Map(Warehouse.From);
            else
                resp.Result = Handler.Typeahead(request.Query, request.IncludeDeleted).Map(Warehouse.From);

            return resp;
        }
    }
}
