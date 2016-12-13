namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/permissions", "GET")]
    [Authenticate]
    public class UserPermissions : IReturn<PermissionsResponse>
    {
    }
}
