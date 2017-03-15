namespace Derprecated.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    // See https://developers.google.com/schemas/reference/order
    public class Order : IPrimaryKeyable, IAuditable, ISoftDeletable
    {
        public Order()
        {
            AcceptedOffers = new List<Offer>();
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        [StringLength(256)]
        public string OrderNumber { get; set; }
        // [References(typeof(Merchant))]
        // public int MerchantId { get; set; }
        // [Reference]
        // public Merchant Merchant { get; set; }
        public decimal Price { get; set; }
        [StringLength(8)]
        public string PriceCurrency { get; set; }
        [Reference]
        public List<Offer> AcceptedOffers { get; set; }
        [StringLength(32)]
        public string Status { get; set; }
        [StringLength(32)]
        public string PaymentMethod { get; set; }
        [StringLength(32)]
        public string PaymentMethodId { get; set; }

        // [References(typeof(Customer))]
        // public int ShippingCustomerId { get; set; }
        // [References(typeof(Customer))]
        // public int BillingCustomerId {get; set;}

        public Customer ShippingCustomer { get; set; }
        public Address ShippingAddress { get; set; }
        public Customer BillingCustomer { get; set; }
        public Address BillingAddress { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        public ulong RowVersion { get; set; }
    }
}
