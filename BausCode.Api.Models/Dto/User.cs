using ServiceStack.Auth;

namespace BausCode.Api.Models.Dto
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public static User From(UserAuth source)
        {
            return new User()
            {
                Id = source.Id,
                Email = source.Email
            };
        }
    }
}