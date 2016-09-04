using BausCode.Api.Models.Attributes;
using ServiceStack;

// ReSharper disable UnusedMember.Global

namespace BausCode.Api.Models.Dto
{
    public class Variant
    {
        [Whitelist]
        public int Id { get; set; }

        public ulong Version { get; set; }

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

        public static Variant From(ProductVariant source)
        {
            return new Variant
            {
                Id = source.Id,
                Version = source.RowVersion
            }.PopulateWith(source.Meta);
        }
    }
}