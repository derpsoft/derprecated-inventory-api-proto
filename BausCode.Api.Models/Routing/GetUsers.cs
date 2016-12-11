namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/users", "GET")]
    [Authenticate]
    public class GetUsers : IReturn<GetUsersResponse>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
