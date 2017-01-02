namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/locations", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanReadLocations)]
    public class GetLocations : IReturn<LocationsResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }
}
