namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/prices/")]
    public class GetPriceOverrides : IReturn<GetPriceOverridesResponse>
    {
    }
}
