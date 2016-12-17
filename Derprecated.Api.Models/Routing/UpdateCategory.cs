namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/categories/{Id}", "PUT")]
    [Authenticate]
    public class UpdateCategory : IReturn<UpdateCategoryResponse>
    {
    }
}
