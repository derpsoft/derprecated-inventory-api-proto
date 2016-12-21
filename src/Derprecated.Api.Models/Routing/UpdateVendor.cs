namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/vendors/{Id}", "PUT, PATCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanUpsertVendors)]
    public class UpdateVendor : IReturn<VendorResponse>
    {
        public int Id { get; set; }
        public Dto.Vendor Vendor { get; set; }
    }
}
