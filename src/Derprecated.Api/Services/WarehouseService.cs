namespace Derprecated.Api.Services
{
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Logging;

    public class WarehouseService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(WarehouseService));

        public object Any(GetWarehouse request)
        {
            var resp = new WarehouseResponse();
            var handler = new WarehouseHandler(Db, CurrentSession);

            resp.Warehouse = Warehouse.From(handler.Get(request.Id));

            return resp;
        }

        public object Any(CreateWarehouse request)
        {
            var resp = new WarehouseResponse();
            var handler = new WarehouseHandler(Db, CurrentSession);
            var newWarehouse = handler.Save(new Models.Warehouse
            {
                Name = request.Warehouse.Name
            });

            resp.Warehouse = Warehouse.From(newWarehouse);

            return resp;
        }

        public object Any(UpdateWarehouse request)
        {
            var resp = new WarehouseResponse();
            var handler = new WarehouseHandler(Db, CurrentSession);
            var update = handler.Save(new Models.Warehouse
            {
                Id = request.Id
            }.PopulateWith(request.Warehouse));

            resp.Warehouse = Warehouse.From(update);


            return resp;
        }
    }
}
