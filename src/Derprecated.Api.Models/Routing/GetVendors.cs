namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/vendors", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanReadVendors)]
    public class GetVendors : IReturn<VendorsResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }
}
