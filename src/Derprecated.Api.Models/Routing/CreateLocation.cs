namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    [Route("/api/v1/locations", "POST")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanUpsertLocations)]
    public class CreateLocation : IReturn<LocationResponse>
    {
        public Location Location { get; set; }
    }
}
