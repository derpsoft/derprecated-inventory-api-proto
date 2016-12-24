namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/users/typeahead", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageUsers, Permissions.CanReadUsers)]
    public class UserTypeahead : IReturn<UsersResponse>
    {
        [Required]
        [StringLength(20)]
        public string Query { get; set; }
    }
}
