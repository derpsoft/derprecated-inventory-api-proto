using System.Collections.Generic;
using BausCode.Api.Models.Attributes;
using BausCode.Api.Models.Dto;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}", "PUT")]
    public class UpdateProduct : IReturn<UpdateProductResponse>
    {
        public int Id { get; set; }

        [Whitelist]
        public ProductMeta Meta { get; set; }

        [Whitelist]
        public List<Variant> Variants { get; set; }
    }
}