namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/users/{Id}", "GET, PUT, PATCH, DELETE")]
    [Route("/api/v1/users", "POST")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Models.Permissions.CanDoEverything, Models.Permissions.CanManageUsers,
         Models.Permissions.CanReadUsers)]
    [RequiresAnyPermission(ApplyTo.Put | ApplyTo.Patch, Models.Permissions.CanDoEverything,
         Models.Permissions.CanManageUsers,
         Models.Permissions.CanUpsertUsers)]
    [RequiresAnyPermission(ApplyTo.Delete, Models.Permissions.CanDoEverything, Models.Permissions.CanManageUsers,
         Models.Permissions.CanDeleteUsers)]
    public sealed class User : IReturn<Dto<User>>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }

        public List<string> Permissions { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        public static User From(UserAuth source)
        {
            var result = new User().PopulateWith(source);

            return result;
        }
    }

    [Route("/api/v1/users", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageUsers,
         Permissions.CanReadUsers)]
    public sealed class Users : IReturn<Dto<List<User>>>
    {
        public int? Skip { get; set; } = 0;
        public int? Take { get; set; } = 25;
    }

    [Route("/api/v1/users/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageUsers, Permissions.CanReadUsers)]
    public sealed class UserCount : IReturn<Dto<long>>
    {
    }

    [Route("/api/v1/users/typeahead", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageUsers, Permissions.CanReadUsers)]
    public sealed class UserTypeahead : IReturn<Dto<List<User>>>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }

    [Route("/api/v1/users/search")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageUsers, Permissions.CanReadUsers)]
    public sealed class UserSearch : QueryDb<UserAuth, Dto<User>>
    {
        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Email",
             ValueFormat = "%{0}%")]
        public string Email { get; set; }

        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }
    }
}
