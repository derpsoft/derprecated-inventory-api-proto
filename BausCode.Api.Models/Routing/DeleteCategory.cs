using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/categories/{Id}", "DELETE")]
    public class DeleteCategory : IReturn<DeleteCategoryResponse>
    {
    }
}