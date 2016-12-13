namespace BausCode.Api.Models.Dto
{
    using ServiceStack;
    using ServiceStack.Auth;

    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public static User From(UserAuth source)
        {
            var result = new User().PopulateWith(source);

            return result;
        }
    }
}
