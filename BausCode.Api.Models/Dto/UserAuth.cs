using System.Collections.Generic;

namespace BausCode.Api.Models.Dto
{
    public class UserAuth
    {
        public int Id { get; set; }
        public string PrimaryEmail { get; set; }
        public string DisplayName { get; set; }
        public List<string> Roles { get; set; }

        public static UserAuth From(ServiceStack.Auth.UserAuth source)
        {
            var userAuth = new UserAuth();

            userAuth.Id = source.Id;
            userAuth.PrimaryEmail = source.PrimaryEmail;
            userAuth.DisplayName = source.DisplayName;
            userAuth.Roles = source.Roles;

            return userAuth;
        }
    }
}