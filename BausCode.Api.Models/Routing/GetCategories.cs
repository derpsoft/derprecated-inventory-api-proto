using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/categories", "GET")]
    public class GetCategories : IReturn<GetCategoriesResponse>
    {
    }
}