namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/categories", "GET")]
    public class GetCategories : IReturn<GetCategoriesResponse>
    {
    }
}
