﻿// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using Attributes;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/products", "POST")]
    [Route("/api/v1/products/{Id}", "GET, DELETE, PUT, PATCH")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanDeleteProducts)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Put | ApplyTo.Patch, Permissions.CanDoEverything, Permissions.CanManageProducts,
        Permissions.CanUpsertProducts)]
    public class Product : IReturn<Dto<Product>>
    {
        public string Barcode { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int Grams { get; set; }
        public int Id { get; set; }

        public List<Image> Images { get; set; }
        public decimal Price { get; set; }

        public decimal QuantityOnHand { get; set; }

        public long? ShopifyId { get; set; }
        public string Sku { get; set; }
        public string Tags { get; set; }

        public string Title { get; set; }
        public string UnitOfMeasure { get; set; } = "each";
        public ulong Version { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }

        public static Product From(Models.Product source)
        {
            return new Product().PopulateWith(source);
        }
    }

    [Route("/api/v1/products/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    public class ProductCount : IReturn<Dto<long>>
    {
        
    }

    [Route("/api/v1/products", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    public class Products : IReturn<Dto<List<Product>>> 
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    public class ProductSearch
    {
        
    }

    [Route("/api/v1/products/typeahead", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    public class ProductTypeahead : IReturn<Dto<Product>>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }
}
