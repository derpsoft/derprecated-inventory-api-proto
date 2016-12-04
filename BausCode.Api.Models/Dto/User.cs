namespace BausCode.Api.Models.Dto
{
    using ServiceStack.Auth;

    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }

        public static User From(UserAuth source)
        {
            return new User
                   {
                       Id = source.Id,
                       Email = source.Email,
                       FirstName = source.FirstName,
                       LastName = source.LastName
                   };
        }
    }
}
