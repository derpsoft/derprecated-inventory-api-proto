namespace Derprecated.Api.Models
{
    using System;

    public class InventoryTraceTransaction : IAuditable
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public int InventoryTransactionId { get; set; }
        public DateTime ModifyDate { get; set; }
        public decimal Quantity { get; set; }
        public ulong RowVersion { get; set; }
        public int UserId { get; set; }
    }
}
