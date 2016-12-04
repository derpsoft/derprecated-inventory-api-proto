namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/categories/{Id}", "PUT")]
    public class UpdateCategory : IReturn<UpdateCategoryResponse>
    {
    }
}
