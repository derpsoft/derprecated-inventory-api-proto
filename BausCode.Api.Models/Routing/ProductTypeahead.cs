using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/typeahead/", "POST")]
    [Route("/api/v1/products/typeahead/{Query}", "GET")]
    public class ProductTypeahead : QueryDb<Product>, IJoin<Product, ProductImage>
    {
        [Required]
        [QueryDbField(Template = "{Field} LIKE {Value}", Field = "Title",
            ValueFormat = "%{0}%")]
        public string Query { get; set; }
    }
}