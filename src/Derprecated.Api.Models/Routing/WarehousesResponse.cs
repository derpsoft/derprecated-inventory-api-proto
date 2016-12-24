namespace Derprecated.Api.Models.Routing
{
    using System.Collections.Generic;
    using Dto;
    using ServiceStack;

    public class WarehousesResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }
}
