﻿using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{id}.json", "PUT")]
    [DataContract]
    public class UpdateProduct : IReturn<ProductResponse>
    {
        [DataMember(IsRequired = true)]
        public long Id { get; set; }

        [DataMember(Name = "product", IsRequired = true)]
        public Product Product { get; set; }
    }
}