using System.Collections.Generic;

namespace BausCode.Api.Models.Dto
{
    public class Profile
    {
        public string PrimaryEmail { get; set; }
        public string DisplayName { get; set; }
        public List<string> Roles { get; set; }

        public static Profile From(Models.UserSession source)
        {
            var userAuth = new Profile();

            userAuth.PrimaryEmail = source.PrimaryEmail;
            userAuth.DisplayName = source.DisplayName;
            userAuth.Roles = source.Roles;

            return userAuth;
        }
    }
}