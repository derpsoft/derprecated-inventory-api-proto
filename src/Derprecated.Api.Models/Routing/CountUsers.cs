namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/users/count", "GET")]
    [Authenticate]
    [RequiredRole(Roles.Admin)]
    public class CountUsers : IReturn<CountResponse>
    {
    }
}
