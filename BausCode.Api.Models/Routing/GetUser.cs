using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/me", "GET")]
    [Authenticate]
    public class GetUser : IReturn<GetUserResponse>
    {
    }
}