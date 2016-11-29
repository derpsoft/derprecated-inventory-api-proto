using System;
using System.Runtime.Serialization;

namespace BausCode.Api.Models.Shopify
{
    [DataContract]
    public abstract class ShopifyObject
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTimeOffset ModifiedAt { get; set; }
    }
}