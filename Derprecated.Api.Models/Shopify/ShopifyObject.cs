namespace Derprecated.Api.Models.Shopify
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class ShopifyObject
    {
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTimeOffset CreatedAt { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
