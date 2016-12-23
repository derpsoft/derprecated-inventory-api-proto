namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Sale
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public decimal Quantity { get; set; }
    }
}
