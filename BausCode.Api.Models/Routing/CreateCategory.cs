namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/categories", "POST")]
    public class CreateCategory : IReturn<CreateCategoryResponse>
    {
    }
}
