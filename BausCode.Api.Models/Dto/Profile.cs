namespace BausCode.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;

    public class Profile
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public List<string> Permissions { get; set; }
        public string PhoneNumber { get; set; }
        public string PrimaryEmail { get; set; }
        public List<string> Roles { get; set; }

        public static Profile From(Models.UserSession source)
        {
            var userAuth = new Profile().PopulateWith(source);

            return userAuth;
        }
    }
}
