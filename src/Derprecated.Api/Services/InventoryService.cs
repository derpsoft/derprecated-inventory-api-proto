namespace Derprecated.Api.Services
{
    using System;
    using Handlers;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack.Logging;

    public class InventoryService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof (InventoryService));

        public object Any(CountInventoryTransactions request)
        {
            var resp = new CountResponse();
            var handler = new InventoryHandler(Db, CurrentSession);

            resp.Count = handler.CountInventoryTransactions();

            return resp;
        }

        public object Any(CreateInventoryTransaction request)
        {
            if (request.Quantity == 0)
                throw new ArgumentOutOfRangeException(nameof(request.Quantity));

            var resp = new InventoryTransactionResponse();
            var handler = new InventoryHandler(Db, CurrentSession);

            if (request.Quantity > 0)
                handler.Receive(request);
            else
                resp.InventoryTransaction = InventoryTransaction.From(handler.Release(request));

            return resp;
        }
    }
}
