namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageProducts, Permissions.CanReadProducts)]
    public class CountProducts : IReturn<CountResponse>
    {
    }
}
