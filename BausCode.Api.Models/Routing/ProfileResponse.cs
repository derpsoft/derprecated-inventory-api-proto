namespace BausCode.Api.Models.Routing
{
    using Dto;

    public class ProfileResponse
    {
        public ProfileResponse()
        {
            Profile = new Dto.Profile();
        }

        public Dto.Profile Profile { get; set; }
    }
}
