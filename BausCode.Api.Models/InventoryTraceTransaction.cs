using System;

namespace BausCode.Api.Models
{
    public class InventoryTraceTransaction : IAuditable
    {
        public int Id { get; set; }
        public int InventoryTransactionId { get; set; }
        public int UserId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}