namespace BausCode.Api.Models.Routing
{
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/vendors", "POST")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanUpsertVendors)]
    public class CreateVendor : IReturn<VendorResponse>
    {
        [Required]
        public string Name { get; set; }
    }
}
