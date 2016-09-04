using BausCode.Api.Models.Routing;

namespace BausCode.Api.Services
{
    public class InventoryService : BaseService
    {
        public object Any(CreateInventoryTransaction request)
        {
            var resp = new CreateInventoryTransactionResponse();
            return resp;
        }
    }
}