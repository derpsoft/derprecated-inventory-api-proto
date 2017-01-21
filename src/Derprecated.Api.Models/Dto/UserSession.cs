namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;

    public class UserSession
    {
        public string DisplayName { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<string> Permissions { get; set; }
        public string PrimaryEmail { get; set; }
        public List<string> Roles { get; set; }

        public static UserSession From(Models.UserSession source)
        {
            var result = new UserSession();

            result.IsAuthenticated = source.IsAuthenticated;
            result.DisplayName = source.DisplayName;
            result.PrimaryEmail = source.PrimaryEmail;
            result.Roles = source.Roles;
            result.Permissions = source.Permissions;

            return result;
        }
    }
}
