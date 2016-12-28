namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/inventory-transactions/search")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageInventory, Permissions.CanReadInventory)]
    public class SearchInventoryTransactions : QueryDb<InventoryTransaction, Dto.InventoryTransaction>
    {
    }
}
