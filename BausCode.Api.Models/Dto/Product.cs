 // ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace BausCode.Api.Models.Dto
{
    using System.Collections.Generic;
    using Attributes;
    using ServiceStack;

    public class Product
    {
        public string Barcode { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int Grams { get; set; }
        public int Id { get; set; }

        public List<Image> Images { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public string Tags { get; set; }

        public string Title { get; set; }
        public ulong Version { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }

        public static Product From(Models.Product source)
        {
            var product = new Product
                          {
                              Id = source.Id,
                              Version = source.RowVersion,
                              Images = source.Images.Map(Image.From)
                          }.PopulateFromPropertiesWithAttribute(source, typeof (WhitelistAttribute));

            return product;
        }
    }
}
