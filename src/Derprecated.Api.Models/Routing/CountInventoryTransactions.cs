namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/inventory-transactions/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageInventory, Permissions.CanReadInventory)]
    public class CountInventoryTransactions : IReturn<CountResponse>
    {
    }
}
