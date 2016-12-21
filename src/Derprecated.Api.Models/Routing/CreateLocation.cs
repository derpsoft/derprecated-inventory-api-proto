namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/locations", "POST")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanUpsertLocations)]
    public class CreateLocation : IReturn<LocationResponse>
    {
        public Dto.Location Location { get; set; }
    }
}
