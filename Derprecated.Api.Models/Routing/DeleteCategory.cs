namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/categories/{Id}", "DELETE")]
    [Authenticate]
    public class DeleteCategory : IReturn<DeleteCategoryResponse>
    {
    }
}
