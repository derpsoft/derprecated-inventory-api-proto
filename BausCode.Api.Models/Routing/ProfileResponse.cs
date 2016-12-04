namespace BausCode.Api.Models.Routing
{
    using Dto;

    public class ProfileResponse
    {
        public ProfileResponse()
        {
            Profile = new Profile();
        }

        public Profile Profile { get; set; }
    }
}
