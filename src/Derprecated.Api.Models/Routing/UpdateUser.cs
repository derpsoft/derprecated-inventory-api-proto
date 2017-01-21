namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using ServiceStack;

    [Route("/api/v1/users/{Id}", "PUT, PATCH")]
    [RequiresAnyPermission(Models.Permissions.CanDoEverything, Models.Permissions.CanManageUsers,
        Models.Permissions.CanUpsertUsers)]
    public class UpdateUser : IReturn<GetUserResponse>
    {
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public List<string> Permissions { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}
