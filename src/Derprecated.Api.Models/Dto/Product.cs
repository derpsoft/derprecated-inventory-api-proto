// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Derprecated.Api.Models.Dto
{
    [Route("/api/v1/products", "POST")]
    [Route("/api/v1/products/{Id}", "GET, DELETE, PUT, PATCH")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageProducts,
        Permissions.CanReadProducts)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything, Permissions.CanManageProducts,
        Permissions.CanDeleteProducts)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Put | ApplyTo.Patch, Permissions.CanDoEverything,
        Permissions.CanManageProducts,
        Permissions.CanUpsertProducts)]
    public class Product : IReturn<Dto<Product>>
    {
        public string Barcode { get; set; }

        public int? CategoryId { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int Grams { get; set; }
        public int Id { get; set; }

        public List<Image> Images { get; set; }

        public bool IncludeDeleted { get; set; } = false;
        public decimal Price { get; set; }

        public decimal QuantityOnHand { get; set; }

        public long? ShopifyId { get; set; }
        public string Sku { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public string UnitOfMeasure { get; set; } = "each";
        public int VendorId { get; set; }
        public ulong Version { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }

        public static Product From(Models.Product source)
        {
            var p = new Product().PopulateWith(source);
            p.Images = source.Images.Map(Image.From);
            return p;
        }
    }

    [Route("/api/v1/products/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    public class ProductCount : IReturn<Dto<long>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    [Route("/api/v1/products", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageProducts,
        Permissions.CanReadProducts)]
    public class Products : IReturn<Dto<List<Product>>>
    {
        public bool IncludeDeleted { get; set; } = false;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    public class ProductSearch
    {
    }

    [Route("/api/v1/products/typeahead", "GET, SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    public class ProductTypeahead : IReturn<Dto<Product>>
    {
        public bool IncludeDeleted { get; set; } = false;

        [StringLength(20)]
        public string Query { get; set; }
    }

    [Route("/api/v1/products/{ProductId}/images/{Id}", "GET, DELETE")]
    [Route("/api/v1/products/{ProductId}/images", "POST")]
    public class ProductImage : IReturn<Dto<Image>>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
    }

    [Route("/api/v1/products/{Id}/images", "GET")]
    public class ProductImages : IReturn<Dto<List<Image>>>
    {
        public int Id { get; set; }
    }
}
