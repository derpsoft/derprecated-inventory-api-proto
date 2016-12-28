﻿namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    /// <summary>
    ///     Used for all inventory operations, including receiving, moving, selling, quantity changes, etc.
    /// </summary>
    [Route("/api/v1/inventory-transaction", "POST")]
    [Authenticate]
    public class CreateInventoryTransaction : IReturn<CreateInventoryTransactionResponse>
    {
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public decimal Quantity { get; set; }
    }
}
