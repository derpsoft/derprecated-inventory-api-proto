namespace Derprecated.Api.Models.Dto.Auth0
{
    public class AppMetadata
    {
        public AppMetadata()
        {
            Authorization = new Authorization();
        }

        public Authorization Authorization { get; set; }
    }
}
