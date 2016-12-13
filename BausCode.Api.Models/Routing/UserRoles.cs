namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/roles", "GET")]
    [Authenticate]
    public class UserRoles : IReturn<RolesResponse>
    {
    }
}
