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

        public object Any(WarehouseCount request)
        {
            var resp = new Dto<long>();
            var handler = new WarehouseHandler(Db, CurrentSession);

            resp.Result = handler.Count();

            return resp;
        }

        public object Get(Warehouse request)
        {
            var resp = new Dto<Warehouse>();
            var handler = new WarehouseHandler(Db, CurrentSession);

            resp.Result = Warehouse.From(handler.Get(request.Id));

            return resp;
        }

        public object Delete(Warehouse request)
        {
            throw new NotImplementedException();
        }

        public object Any(Warehouse request)
        {
            var resp = new Dto<Warehouse>();
            var handler = new WarehouseHandler(Db, CurrentSession);
            var warehouse = new Models.Warehouse().PopulateWith(request);

            resp.Result = Warehouse.From(handler.Save(warehouse));

            return resp;
        }

        public object Any(Warehouses request)
        {
            var resp = new Dto<List<Warehouse>>();
            var handler = new WarehouseHandler(Db, CurrentSession);

            resp.Result = handler.List(request.Skip, request.Take).Map(Warehouse.From);

            return resp;
        }

        public object Any(WarehouseTypeahead request)
        {
            var resp = new Dto<List<Warehouse>>();
            var warehouseHandler = new WarehouseHandler(Db, CurrentSession);
            var searchHandler = new SearchHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = warehouseHandler.List(0, int.MaxValue).Map(Warehouse.From);
            else
                resp.Result = searchHandler.WarehouseTypeahead(request.Query).Map(Warehouse.From);

            return resp;
        }
    }
}
