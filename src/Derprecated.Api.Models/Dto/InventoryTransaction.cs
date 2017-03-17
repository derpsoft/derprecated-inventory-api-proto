namespace Derprecated.Api.Models.Dto
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    public class InventoryTransaction
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public InventoryTransactionTypes TransactionType { get; set; }
        public int UnitOfMeasureId { get; set; }
        public int UserId { get; set; }

        public static InventoryTransaction From(Models.InventoryTransaction source)
        {
            return new InventoryTransaction().PopulateWith(source);
        }
    }

    [Route("/api/v1/inventory-transactions", "GET")]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Post, Permissions.CanDoEverything,
         Permissions.CanManageInventory)]
    public sealed class InventoryTransactions : IReturn<Dto<List<InventoryTransaction>>>
    {
        public List<InventoryTransaction> InventoryTransaction { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }
}
