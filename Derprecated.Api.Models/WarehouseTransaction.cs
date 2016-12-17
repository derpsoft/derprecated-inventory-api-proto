namespace Derprecated.Api.Models
{
    using System;

    public class WarehouseTransaction : IAuditable
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public DateTime ModifyDate { get; set; }
        public decimal Quantity { get; set; }
        public ulong RowVersion { get; set; }
        public string TransactionType { get; set; }
        public int UnitOfMeasureId { get; set; }
    }
}
