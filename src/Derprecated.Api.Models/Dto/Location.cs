namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/locations", "POST")]
    [Route("/api/v1/locations/{Id}", "PUT, PATCH, GET, DELETE")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageLocations,
        Permissions.CanReadLocations)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Patch | ApplyTo.Put, Permissions.CanDoEverything,
        Permissions.CanManageLocations,
        Permissions.CanUpsertLocations)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything, Permissions.CanManageLocations,
        Permissions.CanDeleteLocations)]
    public class Location : IReturn<Dto<Location>>
    {
        public string Bin { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rack { get; set; }
        public string Row { get; set; }
        public ulong RowVersion { get; set; }
        public string Shelf { get; set; }
        public int WarehouseId { get; set; }

        public static Location From(Models.Location source)
        {
            return new Location().PopulateWith(source);
        }
    }

    [Route("/api/v1/locations", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanReadLocations)]
    public class Locations : IReturn<Dto<List<Location>>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/locations/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanReadLocations)]
    public sealed class LocationCount : IReturn<Dto<long>>
    {
    }

    [Route("/api/v1/locations", "SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanReadLocations)]
    public sealed class LocationSearch : QueryDb<Models.Location, Location>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }

        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Row",
            ValueFormat = "%{0}%")]
        public string Row { get; set; }

        [QueryDbField(Term = QueryTerm.Or)]
        public int WarehouseId { get; set; }
    }

    [Route("/api/v1/locations/typeahead", "SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanReadLocations)]
    public sealed class LocationTypeahead : IReturn<Dto<List<Location>>>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }
}
