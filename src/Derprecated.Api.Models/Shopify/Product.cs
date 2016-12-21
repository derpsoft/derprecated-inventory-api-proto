namespace Derprecated.Api.Models.Shopify
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Product : ShopifyObject
    {
        public Product()
        {
            PublishedScope = "web";
        }

        [DataMember(Name = "body_html", EmitDefaultValue = false)]
        public string BodyHtml { get; set; }

        [DataMember(Name = "handle", EmitDefaultValue = false)]
        public string Handle { get; set; }

        [DataMember(Name = "images", EmitDefaultValue = false)]
        public List<Image> Images { get; set; }

        [DataMember(Name = "published", EmitDefaultValue = true)]
        public bool IsPublished { get; set; }

        public List<Option> Options { get; set; }

        [DataMember(Name = "product_type", EmitDefaultValue = false)]
        public string ProductType { get; set; }

        [DataMember(Name = "published_at", EmitDefaultValue = false)]
        public DateTimeOffset PublishedAt { get; set; }

        /// <summary>
        ///     Comma-separated list of the following: web, global, pos
        /// </summary>
        [DataMember(Name = "published_scope", EmitDefaultValue = false)]
        public string PublishedScope { get; set; }

        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public string Tags { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        [DataMember(Name = "variants", EmitDefaultValue = false)]
        public List<Variant> Variants { get; set; }

        [DataMember(Name = "vendor", EmitDefaultValue = false)]
        public string Vendor { get; set; }

        public static Product From(Models.Product source)
        {
            var product = new Product
                          {
                              Id = source.ShopifyId,
                              Title = source.Title,
                              BodyHtml = source.Description,
                              Vendor = source.Vendor,
                              Tags = source.Tags
                          };

            return product;
        }
    }
}
