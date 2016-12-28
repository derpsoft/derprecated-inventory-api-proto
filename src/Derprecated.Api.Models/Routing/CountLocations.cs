namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/locations/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanReadLocations)]
    public class CountLocations : IReturn<CountResponse>
    {
    }
}
