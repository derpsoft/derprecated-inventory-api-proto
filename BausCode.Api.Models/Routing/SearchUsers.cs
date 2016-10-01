using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/users/search", "POST")]
    public class SearchUsers : IReturn<SearchUsersResponse>
    {
        public string Query { get; set; }
    }
}