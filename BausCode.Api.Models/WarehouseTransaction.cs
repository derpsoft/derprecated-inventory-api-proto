using System;

namespace BausCode.Api.Models
{
    public class WarehouseTransaction: IAuditable
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}