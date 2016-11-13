using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BausCode.Api.Models.Shopify
{
    [DataContract]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Product
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "body_html")]
        public string BodyHtml { get; set; }

        [DataMember(Name = "vendor")]
        public string Vendor { get; set; }

        [DataMember(Name = "product_type")]
        public string ProductType { get; set; }

        [DataMember(Name = "handle")]
        public string Handle { get; set; }

        [DataMember(Name = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTimeOffset ModifiedAt { get; set; }

        [DataMember(Name = "published_at")]
        public DateTimeOffset PublishedAt { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }

        [DataMember(Name = "variants")]
        public List<Variant> Variants { get; set; }

        public List<Option> Options { get; set; }

        [DataMember(Name = "images")]
        public List<Image> Images { get; set; }
    }
}