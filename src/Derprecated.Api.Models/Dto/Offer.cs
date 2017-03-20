// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

﻿namespace Derprecated.Api.Models.Dto
{
    public class Offer : IPrimaryKeyable
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }
        public decimal Quantity { get; set; }
        public string ItemCondition { get; set; }
        public string SellerId { get; set; }
        public ulong RowVersion {get;set;}
    }
}
