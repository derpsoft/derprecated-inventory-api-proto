namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/vendors", "POST")]
    [Route("/api/v1/vendors/{Id}", "GET, DELETE, PATCH, PUT")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageVendors,
        Permissions.CanReadVendors)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything, Permissions.CanManageVendors,
        Permissions.CanDeleteVendors)]
    [RequiresAnyPermission(ApplyTo.Put | ApplyTo.Post | ApplyTo.Patch, Permissions.CanDoEverything,
        Permissions.CanManageVendors, Permissions.CanReadVendors)]
    public sealed class Vendor : IReturn<Dto<Vendor>>
    {
        public string ContactAddress { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ulong RowVersion { get; set; }

        public static Vendor From(Models.Vendor source)
        {
            var result = new Vendor().PopulateWith(source);

            return result;
        }
    }

    [Route("/api/v1/vendors/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageVendors,
        Permissions.CanReadVendors)]
    public sealed class VendorCount : IReturn<Dto<long>>
    {
    }

    [Route("/api/v1/vendors", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageVendors,
        Permissions.CanReadVendors)]
    public sealed class Vendors : IReturn<Dto<List<Vendor>>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/vendors", "GET, SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Search, Permissions.CanDoEverything, Permissions.CanManageVendors,
        Permissions.CanReadVendors)]
    public sealed class VendorSearch : QueryDb<Models.Vendor, Vendor>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }

        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Name",
            ValueFormat = "%{0}%")]
        public string Name { get; set; }
    }

    [Route("/api/v1/vendors/typeahead", "GET, SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanReadVendors)]
    public sealed class VendorTypeahead : IReturn<Dto<Vendor>>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }
}
