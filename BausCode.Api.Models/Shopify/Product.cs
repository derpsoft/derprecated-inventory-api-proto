using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BausCode.Api.Models.Attributes;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [DataContract]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Product : ShopifyObject
    {
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        [DataMember(Name = "body_html", EmitDefaultValue = false)]
        public string BodyHtml { get; set; }

        [DataMember(Name = "vendor", EmitDefaultValue = false)]
        public string Vendor { get; set; }

        [DataMember(Name = "product_type", EmitDefaultValue = false)]
        public string ProductType { get; set; }

        [DataMember(Name = "handle", EmitDefaultValue = false)]
        public string Handle { get; set; }

        [DataMember(Name = "published_at", EmitDefaultValue = false)]
        public DateTimeOffset PublishedAt { get; set; }

        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public string Tags { get; set; }

        [DataMember(Name = "variants", EmitDefaultValue = false)]
        public List<Variant> Variants { get; set; }

        public List<Option> Options { get; set; }

        [DataMember(Name = "images", EmitDefaultValue = false)]
        public List<Image> Images { get; set; }

        public static Product From(Models.Product source)
        {
            var product = new Product
            {
                Id = source.ShopifyId
            };

            return product;
        }
    }
}