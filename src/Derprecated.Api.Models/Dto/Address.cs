namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/addresses", "POST")]
    [Route("/api/v1/addresses/{Id}", "GET, PUT, PATCH, DELETE")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything,
         Permissions.CanManageAddresses)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything,
         Permissions.CanManageAddresses)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Patch | ApplyTo.Put, Permissions.CanDoEverything,
         Permissions.CanManageAddresses)]
    public sealed class Address : IReturn<Dto<Address>>, IPrimaryKeyable
    {
        public int Id { get; set; }

        [StringLength(256)]
        public string UserId { get; set; }

        [StringLength(256)]
        public string Line1 { get; set; }

        [StringLength(256)]
        public string Line2 { get; set; }

        [StringLength(256)]
        public string City { get; set; }

        [StringLength(256)]
        public string State { get; set; }

        [StringLength(16)]
        public string Zip { get; set; }

        public bool IncludeDeleted { get; set; } = false;

        [StringLength(32)]
        public string Name { get; set; }

        public ulong RowVersion { get; set; }
    }

    [Route("/api/v1/addresses/count", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageAddresses)]
    public sealed class AddressCount : IReturn<Dto<long>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    [Route("/api/v1/addresses", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageAddresses)]
    public sealed class Addresses : IReturn<Dto<List<Address>>>
    {
        public bool IncludeDeleted { get; set; } = false;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/Addresses/typeahead", "GET, SEARCH")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageAddresses)]
    public sealed class AddressTypeahead : IReturn<Dto<List<Address>>>
    {
        public bool IncludeDeleted { get; set; } = false;

        [StringLength(20)]
        public string Query { get; set; }
    }

    public sealed class AddressSummary : IReturn<Dto<AddressSummary>>
    {
        public string City {get;set;}
        public string State {get;set;}
    }
}
