namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/locations/{Id}", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanReadLocations)]
    public class GetLocation : IReturn<LocationResponse>
    {
        public int Id { get; set; }
    }
}
