namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.Auth;

    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        public List<string> Permissions { get; set; }

        public static User From(UserAuth source)
        {
            var result = new User().PopulateWith(source);

            return result;
        }
    }
}
