using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BausCode.Api.Models.Dto.Shopify
{
    [DataContract]
    public class ProductImage
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "product_id")]
        public long ProductId { get; set; }

        [DataMember(Name = "position")]
        public int Position { get; set; }

        [DataMember(Name = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTimeOffset ModifiedAt { get; set; }

        [DataMember(Name = "src")]
        public string Url { get; set; }

        [DataMember(Name = "variand_ids")]
        public List<int> VariantIds { get; set; }
    }
}