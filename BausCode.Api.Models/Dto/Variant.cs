using ServiceStack;

// ReSharper disable UnusedMember.Global

namespace BausCode.Api.Models.Dto
{
    public class Variant
    {
        public int Id { get; set; }
        public ulong Version { get; set; }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public int Grams { get; set; }
        public string Barcode { get; set; }
        public decimal Weight { get; set; }
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