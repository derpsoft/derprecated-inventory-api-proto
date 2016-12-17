namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/categories", "POST")]
    [Authenticate]
    public class CreateCategory : IReturn<CreateCategoryResponse>
    {
    }
}
