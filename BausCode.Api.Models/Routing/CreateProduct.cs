using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products", "POST")]
    public class CreateProduct : IReturn<CreateProductResponse>
    {
        [Required]
        public Product Product { get; set; }
    }
}