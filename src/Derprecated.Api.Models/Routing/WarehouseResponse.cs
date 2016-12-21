namespace Derprecated.Api.Models.Routing
{
    using Dto;
    using ServiceStack;

    public class WarehouseResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
