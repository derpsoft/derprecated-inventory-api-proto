namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/users/{Id}", "PUT, PATCH")]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageUsers, Permissions.CanUpsertUsers)]
    public class UpdateUser : IReturn<GetUserResponse>
    {
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}
