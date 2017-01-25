namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Sale : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime ModifyDate { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public ulong RowVersion { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Total { get; set; }

        public int UserAuthId { get; set; }
        public int VendorId { get; set; }
    }
}
