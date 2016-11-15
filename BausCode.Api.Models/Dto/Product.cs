using System.Collections.Generic;
using BausCode.Api.Models.Attributes;
using ServiceStack;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace BausCode.Api.Models.Dto
{
    public class Product
    {
        public int Id { get; set; }
        public ulong Version { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public int Grams { get; set; }
        public string Barcode { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }
        public string Color { get; set; }

        public List<Image> Images { get; set; }

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