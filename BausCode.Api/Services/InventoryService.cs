using BausCode.Api.Models.Routing;

namespace BausCode.Api.Services
{
    public class InventoryService : BaseService
    {
        public CreateInventoryTransactionResponse Any(CreateInventoryTransaction request)
        {
            var resp = new CreateInventoryTransactionResponse();
            return resp;
        }
    }
}