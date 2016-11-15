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
            Images = new List<Image>();
        }

        public int Id { get; set; }

        [Whitelist]
        public List<Image> Images { get; set; }
    }
}