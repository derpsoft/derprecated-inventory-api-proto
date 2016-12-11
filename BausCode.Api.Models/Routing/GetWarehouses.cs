namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses", "GET")]
    [Authenticate]
    public class GetWarehouses : IReturn<GetWarehousesResponse>
    {
    }
}
