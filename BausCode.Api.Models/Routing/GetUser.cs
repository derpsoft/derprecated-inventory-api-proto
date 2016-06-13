using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/me", "GET")]
    [Authenticate]
    public class GetUser : IReturn<GetUserResponse>
    {
    }
}