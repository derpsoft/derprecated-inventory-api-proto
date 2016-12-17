namespace Derprecated.Api.Services
{
    using System;
    using Handlers;
    using Models.Routing;
    using ServiceStack.Logging;

    public class InventoryService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (InventoryService));

        public object Any(CreateInventoryTransaction request)
        {
            if (request.Quantity == 0)
                throw new ArgumentOutOfRangeException(nameof(request.Quantity));

            var resp = new CreateInventoryTransactionResponse();
            var handler = new InventoryHandler(Db, CurrentSession);

            if (request.Quantity > 0)
                handler.Receive(request);
            else
                handler.Release(request);

            return resp;
        }
    }
}
