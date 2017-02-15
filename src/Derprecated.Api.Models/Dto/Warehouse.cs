namespace Derprecated.Api.Models.Dto
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/warehouses", "POST")]
    [Route("/api/v1/warehouses/{Id}", "GET, PUT, PATCH, DELETE")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageWarehouses,
         Permissions.CanReadWarehouses)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Put | ApplyTo.Patch, Permissions.CanDoEverything,
         Permissions.CanManageWarehouses, Permissions.CanUpsertWarehouses)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything, Permissions.CanManageWarehouses,
         Permissions.CanDeleteWarehouses)]
    public class Warehouse : IReturn<Dto<Warehouse>>
    {
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
        public bool IncludeDeleted { get; set; } = false;
        public DateTime ModifyDate { get; set; }
        public string Name { get; set; }
        public ulong RowVersion { get; set; }

        public static Warehouse From(Models.Warehouse source)
        {
            return new Warehouse().PopulateWith(source);
        }
    }

    [Route("/api/v1/warehouses/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanReadWarehouses)]
    public class WarehouseCount : IReturn<Dto<long>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    [Route("/api/v1/warehouses", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanReadWarehouses)]
    public class Warehouses : IReturn<Dto<List<Warehouse>>>
    {
        public bool IncludeDeleted { get; set; } = false;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/warehouses", "SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanReadWarehouses)]
    public class WarehouseSearch : QueryDb<Warehouse, Warehouse>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }

        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Name",
             ValueFormat = "%{0}%")]
        public string Name { get; set; }
    }

    [Route("/api/v1/warehouses/typeahead", "SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanReadWarehouses)]
    public class WarehouseTypeahead : IReturn<Dto<List<Warehouse>>>
    {
        public bool IncludeDeleted { get; set; } = false;

        [StringLength(20)]
        public string Query { get; set; }
    }
}
