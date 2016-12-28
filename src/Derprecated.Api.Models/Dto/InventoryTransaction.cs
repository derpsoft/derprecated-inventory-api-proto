namespace Derprecated.Api.Models.Dto
{
    using System;
    using ServiceStack;

    public class InventoryTransaction
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public InventoryTransactionTypes TransactionType { get; set; }
        public int UnitOfMeasureId { get; set; }
        public int UserId { get; set; }

        public static InventoryTransaction From(Models.InventoryTransaction source)
        {
            return new InventoryTransaction().PopulateWith(source);
        }
    }
}
