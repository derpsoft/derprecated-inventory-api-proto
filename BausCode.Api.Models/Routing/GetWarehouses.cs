using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/warehouses", "GET")]
    public class GetWarehouses : IReturn<GetWarehousesResponse>
    {
    }
}