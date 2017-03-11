namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    // See https://developers.google.com/schemas/reference/order
    public class Order : IPrimaryKeyable, IAuditable, ISoftDeletable
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        [References(typeof(Merchant))]
        public int MerchantId { get; set; }
        [Reference]
        public Merchant Merchant { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }
        [Reference]
        public List<Offer> AcceptedOffers { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentMethodId { get; set; }
        [References(typeof(Customer))]
        public int CustomerId { get; set; }
        [Reference]
        public Customer Customer { get; set; }
        public Address BillingAddress { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        public ulong RowVersion { get; set; }
    }
}
