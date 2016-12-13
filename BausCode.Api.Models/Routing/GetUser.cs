namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/users/{Id}", "GET")]
    [Authenticate]
    [RequiredRole(Roles.Admin)]
    public class GetUser : IReturn<GetUserResponse>
    {
        public int Id { get; set; }
    }
}
