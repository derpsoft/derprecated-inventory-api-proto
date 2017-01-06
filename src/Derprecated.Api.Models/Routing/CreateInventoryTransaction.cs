namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    /// <summary>
    ///     Used for all inventory operations, including receiving, moving, selling, quantity changes, etc.
    /// </summary>
    [Route("/api/v1/inventory-transactions", "POST")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageInventory, Permissions.CanUpsertInventory)]
    public class CreateInventoryTransaction : IReturn<CreateInventoryTransactionResponse>
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
