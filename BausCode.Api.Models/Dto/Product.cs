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

        public List<Variant> Variants { get; set; }
        public List<Image> Images { get; set; }

        public static Product From(Models.Product source)
        {
            return new Product
            {
                Id = source.Id,
                Version = source.RowVersion,
                Variants = source.Variants.Map(Variant.From),
                Images = source.Images.Map(Image.From)
            }.PopulateFromPropertiesWithAttribute(source.Meta, typeof (WhitelistAttribute));
        }
    }
}