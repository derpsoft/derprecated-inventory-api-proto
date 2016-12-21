namespace Derprecated.Api.Models.Routing
{
    public class ProfileResponse
    {
        public ProfileResponse()
        {
            Profile = new Dto.Profile();
        }

        public Dto.Profile Profile { get; set; }
    }
}
