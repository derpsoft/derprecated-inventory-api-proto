using BausCode.Api.Models.Attributes;

namespace BausCode.Api.Models
{
    public class ProductVariantMeta
    {
        [Whitelist]
        public string Title { get; set; }

        [Whitelist]
        public decimal Price { get; set; }

        [Whitelist]
        public string Sku { get; set; }

        [Whitelist]
        public int Grams { get; set; }

        [Whitelist]
        public string Barcode { get; set; }

        [Whitelist]
        public decimal Weight { get; set; }

        [Whitelist]
        public string WeightUnit { get; set; }
    }
}