namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;

    [Route("/api/v1/me", "GET, PUT, POST, PATCH")]
    [Route("/api/v1/profile", "GET, PUT, POST, PATCH")]
    [EnsureHttps(SkipIfDebugMode = true, SkipIfXForwardedFor = true)]
    [Authenticate]
    [RequiredPermission(Models.Permissions.CanLogin)]
    public class Profile : IReturn<Dto<Profile>>
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Permissions { get; set; }
        public string PhoneNumber { get; set; }
        public string PrimaryEmail { get; set; }
        public List<string> Roles { get; set; }
        public string UserName { get; set; }

        public static Profile From(Models.UserSession source)
        {
            return new Profile().PopulateWith(source);
        }
    }
}
