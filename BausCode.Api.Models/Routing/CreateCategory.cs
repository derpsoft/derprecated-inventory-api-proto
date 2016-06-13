using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/categories", "POST")]
    public class CreateCategory : IReturn<CreateCategoryResponse>
    {
    }
}