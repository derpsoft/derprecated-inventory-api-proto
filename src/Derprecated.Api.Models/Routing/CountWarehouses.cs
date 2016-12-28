namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanReadWarehouses)]
    public class CountWarehouses : IReturn<CountResponse>
    {
    }
}
