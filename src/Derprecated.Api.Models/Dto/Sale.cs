namespace Derprecated.Api.Models.Dto
{
    using System;
    using ServiceStack;

    public class Sale
    {
        public int Id { get; set; }
        public int InventoryTransactionId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Total { get; set; }
        public int VendorId { get; set; }

        public static Sale From(Models.Sale source)
        {
            return new Sale().PopulateWith(source);
        }
    }
}
