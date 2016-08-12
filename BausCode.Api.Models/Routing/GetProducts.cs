﻿using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products", "GET")]
    public class GetProducts : IReturn<GetProductsResponse>
    {
        public bool? MetaOnly { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}