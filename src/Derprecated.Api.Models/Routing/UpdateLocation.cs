namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/locations/{Id}", "PUT, PATCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanUpsertLocations)]
    public class UpdateLocation : IReturn<LocationResponse>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Location Location { get; set; }
    }
}
