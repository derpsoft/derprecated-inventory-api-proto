namespace Derprecated.Api.Models.Dto
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/sales", "POST")]
    [Route("/api/v1/vendors/{Id}", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageSales,
        Permissions.CanReadSales)]
    [RequiresAnyPermission(ApplyTo.Post, Permissions.CanDoEverything, Permissions.CanManageSales,
        Permissions.CanUpsertSales)]
    public class Sale : IReturn<Dto<Sale>>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Total { get; set; }
        public int UserAuthId { get; set; }

        public static Sale From(Models.Sale source)
        {
            return new Sale().PopulateWith(source);
        }
    }

    [Route("/api/v1/sales", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageSales,
        Permissions.CanReadSales)]
    public class Sales : IReturn<Dto<List<Sale>>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/sales/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageSales,
        Permissions.CanReadSales)]
    public class SaleCount : IReturn<Dto<long>>
    {
    }

    [Route("/api/v1/sales/typeahead", "GET, SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageSales,
        Permissions.CanReadSales)]
    public sealed class SaleTypeahead : IReturn<Dto<List<Sale>>>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }
}
