using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}", "GET")]
    public class GetProduct : IReturn<ProductResponse>
    {
        [Required]
        public int Id { get; set; }

        public List<string> Fields { get; set; }
    }
}