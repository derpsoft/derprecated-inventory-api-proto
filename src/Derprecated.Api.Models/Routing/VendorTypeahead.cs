namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/vendors/typeahead", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanReadVendors)]
    public class VendorTypeahead : IReturn<VendorsResponse>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }
}
