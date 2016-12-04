namespace BausCode.Api.Models
{
    using System;

    public class InventoryTransaction : IAuditable
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public ulong RowVersion { get; set; }
        public InventoryTransactionTypes TransactionType { get; set; }
        public int UnitOfMeasureId { get; set; }
        public int UserId { get; set; }
    }
}
