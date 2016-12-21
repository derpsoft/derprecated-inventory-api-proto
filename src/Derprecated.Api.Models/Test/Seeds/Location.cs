namespace Derprecated.Api.Models.Test.Seeds
{
    using System.Collections.Generic;

    public static class Location
    {
        public static readonly Models.Location EmptyLocation = new Models.Location
                                                               {
                                                                   Id = 1,
                                                                   WarehouseId = 0,
                                                                   Bin = "0",
                                                                   Rack = "0",
                                                                   Shelf = "0"
                                                               };

        public static readonly Models.Location TestRack = new Models.Location
                                                          {
                                                              Id = 2,
                                                              WarehouseId = Warehouse.ElMonteWarehouse.Id,
                                                              Bin = "1",
                                                              Rack = "1",
                                                              Shelf = "1"
                                                          };

        public static List<Models.Location> Basic = new List<Models.Location>
                                                    {
                                                        EmptyLocation,
                                                        TestRack
                                                    };
    }
}
