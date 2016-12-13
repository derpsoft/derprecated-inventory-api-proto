namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/me", "GET")]
    [Route("/api/v1/profile", "GET")]
    [Authenticate]
    public class GetProfile : IReturn<GetUserResponse>
    {
    }
}
