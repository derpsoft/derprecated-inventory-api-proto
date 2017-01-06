namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class LocationResponse
    {
        public Location Location { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
