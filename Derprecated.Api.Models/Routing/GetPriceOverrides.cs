namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/prices/")]
    [Authenticate]
    public class GetPriceOverrides : IReturn<GetPriceOverridesResponse>
    {
    }
}
