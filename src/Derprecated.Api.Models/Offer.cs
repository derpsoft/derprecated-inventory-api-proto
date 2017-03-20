namespace Derprecated.Api.Models
{
    using System;
    using ServiceStack.DataAnnotations;

    public class Offer : IPrimaryKeyable, IAuditable
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [Reference]
        public Order Order {get;set;}
        public int ProductId { get; set; }
        [Reference]
        public Product Product {get;set;}
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }
        public decimal Quantity { get; set; }
        public string ItemCondition { get; set; }

        [Index]
        public string SellerId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}
