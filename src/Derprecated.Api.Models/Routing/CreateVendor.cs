namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/vendors", "POST")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanUpsertVendors)]
    public class CreateVendor : IReturn<VendorResponse>
    {
        public string ContactAddress { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Name { get; set; }
    }
}
