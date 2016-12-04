namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses", "GET")]
    public class GetWarehouses : IReturn<GetWarehousesResponse>
    {
    }
}
