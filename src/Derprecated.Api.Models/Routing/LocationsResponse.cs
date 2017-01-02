namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using Dto;
    using ServiceStack;

    public class LocationsResponse
    {
        public List<Location> Locations { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
