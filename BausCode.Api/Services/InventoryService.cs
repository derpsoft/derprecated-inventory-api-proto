using BausCode.Api.Models.Routing;
using ServiceStack.Logging;

namespace BausCode.Api.Services
{
    public class InventoryService : BaseService
    {
        protected static ILog Log = LogManager.GetLogger(typeof(InventoryService));

        public object Any(CreateInventoryTransaction request)
        {
            var resp = new CreateInventoryTransactionResponse();
            return resp;
        }
    }
}