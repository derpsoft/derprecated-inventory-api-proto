// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

﻿namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using System;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/orders", "POST")]
    [Route("/api/v1/orders/{Id}", "GET, DELETE, PUT, PATCH")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageOrders)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Put | ApplyTo.Patch, Permissions.CanDoEverything, Permissions.CanManageOrders)]
    public class Order : IReturn<Dto<Order>>, IPrimaryKeyable
    {
        public Order()
        {
            Offers = new List<Offer>();
        }

        public int Id { get; set; }
        public bool IncludeDeleted { get; set; } = false;
        public ulong OrderNumber { get; set; }
        public string OrderKey { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }
        // public int MerchantId {get;set;}
        // public Merchant Merchant {get;set;}
        public int ShippingCustomerId {get;set;}
        public Customer ShippingCustomer { get; set; }
        public int BillingCustomerId {get;set;}
        public Customer BillingCustomer { get; set; }
        public List<Offer> Offers { get; set; }
        public Address ShippingAddress {get;set;}
        public Address BillingAddress { get; set; }
        public string Status { get; set; }
        public string PaymentMethod {get;set;}
        public string PaymentMethodId {get;set;}
        public DateTime CreateDate {get;set;}
        public DateTime? BillDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public ulong RowVersion { get; set; }
    }

    [Route("/api/v1/orders", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageOrders)]
    public class Orders : IReturn<Dto<List<Order>>>
    {
      public bool IncludeDeleted { get; set; } = false;
      public int Skip { get; set; } = 0;
      public int Take { get; set; } = 25;
    }

    [Route("/api/v1/orders/count", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageOrders)]
    public sealed class OrderCount : IReturn<Dto<long>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    [Route("/api/v1/orders/{Id}/billing", "POST")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageOrders)]
    public sealed class OrderBillingCaptured: IReturn<Dto<Order>>
    {
        public int Id {get;set;}
        public string Token {get;set;}
    }

    [Route("/api/v1/orders/{Id}/shipped", "POST")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageOrders)]
    public sealed class OrderShipped : IReturn<Dto<Order>>
    {
        public int Id {get;set;}
    }

    [Route("/api/v1/orders/summary/{Key}/{OrderNumber}", "GET")]
    public sealed class OrderSummary : IReturn<Dto<OrderSummary>>
    {
        // Get this from the order number. Order numbers are of the format Prefix-Id, like DERP-1234. 1234 would be the Id.
        public ulong OrderNumber {get;set;}
        public string Key {get;set;}

        public string OrderStatus {get;set;}
        public decimal Price {get;set;}
        public string PriceCurrency {get;set;}
        public string Status { get; set; }
        public string PaymentMethod {get;set;}
        public string PaymentMethodId {get;set;}
        public AddressSummary ShippingAddress {get;set;}
        public AddressSummary BillingAddress {get;set;}
        public DateTime CreateDate {get;set;}
        public DateTime? BillDate { get; set; }
        public DateTime? ShipDate { get; set; }
    }
}
