namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/vendors/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageVendors, Permissions.CanReadVendors)]
    public class CountVendors : IReturn<CountResponse>
    {
    }
}
