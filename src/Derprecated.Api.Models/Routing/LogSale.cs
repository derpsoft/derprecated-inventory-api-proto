namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/sales", "POST")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageInventory, Permissions.CanUpsertInventory)]
    public class LogSale : IReturn<SaleResponse>
    {
        [Required]
        public int InventoryTransactionId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public int VendorId { get; set; }
    }
}
