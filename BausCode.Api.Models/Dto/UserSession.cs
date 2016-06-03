using System.Collections.Generic;

namespace BausCode.Api.Models.Dto
{
    public class UserSession
    {
        public bool IsAuthenticated { get; set; }
        public string DisplayName { get; set; }
        public string PrimaryEmail { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }

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