namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses/{Id}", "GET")]
    public class GetWarehouse : IReturn<GetWarehouseResponse>
    {
    }
}
