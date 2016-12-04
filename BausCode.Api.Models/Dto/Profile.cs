namespace BausCode.Api.Models.Dto
{
    using System.Collections.Generic;

    public class Profile
    {
        public string DisplayName { get; set; }
        public string PrimaryEmail { get; set; }
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
