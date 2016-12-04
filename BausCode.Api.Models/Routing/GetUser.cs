namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/me", "GET")]
    [Authenticate]
    public class GetUser : IReturn<GetUserResponse>
    {
    }
}
