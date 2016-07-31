using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/categories/{Id}", "PUT")]
    public class UpdateCategory : IReturn<UpdateCategoryResponse>
    {
    }
}