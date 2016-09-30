using System;
using System.Runtime.Serialization;

namespace BausCode.Api.Models.Dto.Shopify
{
    [DataContract]
    public class ProductVariant
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "product_id")]
        public long ProductShopifyId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "price")]
        public string Price { get; set; }

        [DataMember(Name = "sku")]
        public string Sku { get; set; }

        [DataMember(Name = "position")]
        public int Position { get; set; }

        [DataMember(Name = "grams")]
        public int Grams { get; set; }

        [DataMember(Name = "inventory_policy")]
        public string InventoryPolicy { get; set; }

        [DataMember(Name = "compare_at_price")]
        public string CompareAtPrice { get; set; }

        [DataMember(Name = "inventory_management")]
        public string InventoryManagement { get; set; }

        [DataMember(Name = "option1")]
        public string Option1 { get; set; }

        [DataMember(Name = "option2")]
        public string Option2 { get; set; }

        [DataMember(Name = "option3")]
        public string Option3 { get; set; }

        [DataMember(Name = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [DataMember(Name = "taxable")]
        public bool Taxable { get; set; }

        [DataMember(Name = "barcode")]
        public string Barcode { get; set; }

        [DataMember(Name = "image_id")]
        public int ImageId { get; set; }

        [DataMember(Name = "inventory_quantity")]
        public int InventoryQuantity { get; set; }

        [DataMember(Name = "weight")]
        public decimal Weight { get; set; }

        [DataMember(Name = "weight_unit")]
        public string WeightUnit { get; set; }

        [DataMember(Name = "old_inventory_quantity")]
        public int OldInventoryQuantity { get; set; }

        [DataMember(Name = "requires_shipping")]
        public bool RequiresShipping { get; set; }
    }
}