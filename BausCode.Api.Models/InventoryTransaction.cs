using System;

namespace BausCode.Api.Models
{
    public class InventoryTransaction : IAuditable
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public InventoryTransactionTypes TransactionType { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}