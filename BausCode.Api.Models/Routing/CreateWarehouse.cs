using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/warehouses", "POST")]
    public class CreateWarehouse : IReturn<CreateWarehouseResponse>
    {
    }
}