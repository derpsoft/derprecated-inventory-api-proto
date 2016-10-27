using System.Collections.Generic;
using BausCode.Api.Models.Attributes;
using BausCode.Api.Models.Dto;
using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}", "PUT")]
    public class UpdateProduct : IReturn<UpdateProductResponse>
    {
        public UpdateProduct()
        {
            Variants = new List<Dto.Variant>();
            Images = new List<Image>();
        }

        public int Id { get; set; }

        [Whitelist]
        public ProductMeta Meta { get; set; }

        [Whitelist]
        public List<Dto.Variant> Variants { get; set; }

        [Whitelist]
        public List<Image> Images { get; set; }
    }
}