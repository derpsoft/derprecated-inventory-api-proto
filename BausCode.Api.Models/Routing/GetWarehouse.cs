using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/warehouses/{Id}", "GET")]
    public class GetWarehouse : IReturn<GetWarehouseResponse>
    {
    }
}