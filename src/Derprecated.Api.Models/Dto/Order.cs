// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

﻿namespace Derprecated.Api.Models.Dto
{
    using ServiceStack;
    using System.Collections.Generic;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/orders", "POST")]
    [Route("/api/v1/orders/{Id}", "GET, DELETE, PUT, PATCH")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageOrders)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Put | ApplyTo.Patch, Permissions.CanDoEverything, Permissions.CanManageOrders)]
    public class Order : IReturn<Dto<Order>>, IPrimaryKeyable
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }
        public int MerchantId {get;set;}
        public Merchant Merchant {get;set;}
        public int CustomerId {get;set;}
        public Customer Customer { get; set; }
        public List<Offer> AcceptedOffers { get; set; }
        public Address BillingAddress { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMethod {get;set;}
        public string PaymentMethodId {get;set;}

        public static Order From(Models.Order source)
        {
            var dest = new Order().PopulateWith(source);
            dest.Merchant = source.Merchant.ConvertTo<Merchant>();
            dest.Customer = source.Customer.ConvertTo<Customer>();
            dest.AcceptedOffers = source.AcceptedOffers.ConvertAll(x => x.ConvertTo<Offer>());
            return dest;
        }
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
}
