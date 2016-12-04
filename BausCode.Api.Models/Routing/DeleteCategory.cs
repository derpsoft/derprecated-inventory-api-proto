namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/categories/{Id}", "DELETE")]
    public class DeleteCategory : IReturn<DeleteCategoryResponse>
    {
    }
}
