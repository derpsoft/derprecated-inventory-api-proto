namespace Derprecated.Api.Models.Dto
{
    using ServiceStack;

    [Route("/api/v1/vendors", "GET, PUT, POST, PATCH")]
    [Route("/api/v1/vendors/{Id}", "GET, DELETE")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageVendors,
        Permissions.CanReadVendors)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything, Permissions.CanManageVendors,
        Permissions.CanDeleteVendors)]
    [RequiresAnyPermission(ApplyTo.Put | ApplyTo.Post | ApplyTo.Patch, Permissions.CanDoEverything,
        Permissions.CanManageVendors, Permissions.CanReadVendors)]
    public class Vendor
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
}
