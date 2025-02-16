﻿namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/vendors", "POST")]
    [Route("/api/v1/vendors/{Id}", "GET, DELETE, PATCH, PUT")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageVendors,
         Permissions.CanReadVendors)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything, Permissions.CanManageVendors,
         Permissions.CanDeleteVendors)]
    [RequiresAnyPermission(ApplyTo.Put | ApplyTo.Post | ApplyTo.Patch, Permissions.CanDoEverything,
         Permissions.CanManageVendors, Permissions.CanUpsertVendors)]
    public sealed class Vendor : IReturn<Dto<Vendor>>, IPrimaryKeyable
    {
        public string ContactAddress { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public int Id { get; set; }
        public bool IncludeDeleted { get; set; } = false;
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
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageVendors,
         Permissions.CanReadVendors)]
    public sealed class VendorCount : IReturn<Dto<long>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    [Route("/api/v1/vendors", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageVendors,
         Permissions.CanReadVendors)]
    public sealed class Vendors : IReturn<Dto<List<Vendor>>>
    {
        public bool IncludeDeleted { get; set; } = false;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/vendors", "GET, SEARCH")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
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
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanReadVendors)]
    public sealed class VendorTypeahead : IReturn<Dto<Vendor>>
    {
        public bool IncludeDeleted { get; set; } = false;

        [StringLength(20)]
        public string Query { get; set; }
    }
}
