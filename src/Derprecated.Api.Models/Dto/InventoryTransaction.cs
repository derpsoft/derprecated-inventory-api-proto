namespace Derprecated.Api.Models.Dto
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    [Route("/api/v1/inventory-transactions", "POST")]
    [Route("/api/v1/inventory-transactions/{Id}", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageInventory)]
    [RequiresAnyPermission(ApplyTo.Post, Permissions.CanDoEverything, Permissions.CanManageInventory)]
    public class InventoryTransaction : IReturn<Dto<InventoryTransaction>>
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public InventoryTransactionTypes TransactionType { get; set; }
        public string UserAuthId { get; set; }

        public static InventoryTransaction From(Models.InventoryTransaction source)
        {
            return new InventoryTransaction().PopulateWith(source);
        }
    }

    [Route("/api/v1/inventory-transactions", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Post, Permissions.CanDoEverything,
         Permissions.CanManageInventory)]
    public sealed class InventoryTransactions : IReturn<Dto<List<InventoryTransaction>>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/inventory-transactions/count", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageInventory)]
    public sealed class InventoryTransactionCount : IReturn<Dto<long>>
    {
    }
}
