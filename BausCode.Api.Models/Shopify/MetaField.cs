using System;
using System.Runtime.Serialization;

namespace BausCode.Api.Models.Shopify
{
    [DataContract]
    public class Metafield : ShopifyObject
    {
        [DataMember(Name = "owner_id", EmitDefaultValue = false)]
        public long? OwnerId { get; set; }

        [DataMember(Name = "owner_resource", EmitDefaultValue = false)]
        public string OwnerResource { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "key", EmitDefaultValue = false)]
        public string Key { get; set; }

        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        [DataMember(Name = "value_type", EmitDefaultValue = false)]
        public string ValueType { get; set; }

        [DataMember(Name = "namespace", EmitDefaultValue = false)]
        public string Namespace { get; set; }
    }
}