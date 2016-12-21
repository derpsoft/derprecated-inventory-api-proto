namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class LocationResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public Location Location { get; set; }
    }
}
