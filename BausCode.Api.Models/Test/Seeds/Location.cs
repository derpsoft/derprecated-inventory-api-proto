using System.Collections.Generic;

namespace BausCode.Api.Models.Test.Seeds
{
    public static class Location
    {
        public static readonly Models.Location EmptyLocation = new Models.Location
        {
            Id = 1,
            WarehouseId = 0,
            Bin = 0,
            Rack = 0,
            Shelf = 0
        };

        public static List<Models.Location> Basic = new List<Models.Location>
        {
            EmptyLocation
        };
    }
}