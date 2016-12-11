namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses/{Id}", "GET")]
    [Authenticate]
    public class GetWarehouse : IReturn<GetWarehouseResponse>
    {
    }
}
