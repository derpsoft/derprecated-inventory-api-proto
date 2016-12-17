namespace Derprecated.Api.Models.Shopify
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Image : ShopifyObject
    {
        [DataMember(Name = "metafields", EmitDefaultValue = false)]
        public List<Metafield> MetaFields { get; set; }

        [DataMember(Name = "position", EmitDefaultValue = false)]
        public int Position { get; set; }

        [DataMember(Name = "product_id", EmitDefaultValue = false)]
        public long? ProductId { get; set; }

        /// <summary>
        ///     The Url of the image.
        ///     When GETting this from the Shopify API, it points to a Shopify CDN url.
        ///     When POSTing this to the Shopify API, it's a URL to a publicly accessible image, which
        ///     the Shopify API will then download the image from.
        /// </summary>
        [DataMember(Name = "src", EmitDefaultValue = false)]
        public string Url { get; set; }

        [DataMember(Name = "variant_ids", EmitDefaultValue = false)]
        public List<long> VariantIds { get; set; }
    }
}
