namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    /// <summary>
    ///     Used for all inventory operations, including receiving, moving, selling, quantity changes, etc.
    /// </summary>
    [Route("/api/v1/inventory-transaction", "POST")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CreateInventoryTransaction : IReturn<CreateInventoryTransactionResponse>
    {
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public decimal Quantity { get; set; }
    }
}
