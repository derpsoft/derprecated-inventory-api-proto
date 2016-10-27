using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/prices/")]
    public class GetPriceOverrides : IReturn<GetPriceOverridesResponse>
    {

    }
}