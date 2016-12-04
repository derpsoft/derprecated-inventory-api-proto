namespace BausCode.Api.Models.Shopify
{
    using System.Runtime.Serialization;
    using Attributes;

    [DataContract]
    public class Variant : ShopifyObject
    {
        [DataMember(Name = "barcode", EmitDefaultValue = false)]
        [Whitelist]
        public string Barcode { get; set; }

        [DataMember(Name = "compare_at_price", EmitDefaultValue = false)]
        public string CompareAtPrice { get; set; }

        [DataMember(Name = "grams", EmitDefaultValue = false)]
        public int Grams { get; set; }

        [DataMember(Name = "image_id", EmitDefaultValue = false)]
        public int ImageId { get; set; }

        [DataMember(Name = "inventory_management", EmitDefaultValue = false)]
        public string InventoryManagement { get; set; }

        [DataMember(Name = "inventory_policy", EmitDefaultValue = false)]
        public string InventoryPolicy { get; set; }

        [DataMember(Name = "inventory_quantity", EmitDefaultValue = false)]
        public int InventoryQuantity { get; set; }

        [DataMember(Name = "old_inventory_quantity", EmitDefaultValue = false)]
        public int OldInventoryQuantity { get; set; }

        [DataMember(Name = "option1", EmitDefaultValue = false)]
        public string Option1 { get; set; }

        [DataMember(Name = "option2", EmitDefaultValue = false)]
        public string Option2 { get; set; }

        [DataMember(Name = "option3", EmitDefaultValue = false)]
        public string Option3 { get; set; }

        [DataMember(Name = "position", EmitDefaultValue = false)]
        public int Position { get; set; }

        [DataMember(Name = "price", EmitDefaultValue = false)]
        [Whitelist]
        public string Price { get; set; }

        [DataMember(Name = "product_id", EmitDefaultValue = false)]
        public long ProductShopifyId { get; set; }

        [DataMember(Name = "requires_shipping", EmitDefaultValue = false)]
        public bool RequiresShipping { get; set; }

        [DataMember(Name = "sku", EmitDefaultValue = false)]
        [Whitelist]
        public string Sku { get; set; }

        [DataMember(Name = "taxable", EmitDefaultValue = false)]
        public bool Taxable { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        [DataMember(Name = "weight", EmitDefaultValue = false)]
        [Whitelist]
        public decimal Weight { get; set; }

        /// <summary>
        ///     Acceptable values are one of: lb, kg, oz, g
        /// </summary>
        [DataMember(Name = "weight_unit", EmitDefaultValue = false)]
        [Whitelist]
        public string WeightUnit { get; set; }
    }
}
