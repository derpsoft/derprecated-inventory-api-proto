﻿namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    public class ProductResponse
    {
        public Dto.Product Product { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
