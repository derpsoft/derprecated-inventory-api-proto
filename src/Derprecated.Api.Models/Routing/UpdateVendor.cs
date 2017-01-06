namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/vendors/{Id}", "PUT, PATCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanUpsertVendors)]
    public class UpdateVendor : IReturn<VendorResponse>
    {
        public string ContactAddress { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ulong RowVersion { get; set; }
    }
}
