using System;
using System.Collections.Generic;
using System.Drawing;

namespace BausCode.Api.Models.Test.Seeds
{
    public static class Product
    {
        public static readonly Models.Product EmptyProduct = new Models.Product
        {
            Id = 1,
            Barcode = "",
            Color = Color.Transparent.Name,
            CreateDate = DateTime.Now,
            Description = "",
            Grams = 0,
            Images = new List<ProductImage>(),
            ModifyDate = DateTime.Now,
            Price = 0m,
            RowVersion = 1,
            Tags = "",
            Title = "",
            Sku = "",
            Weight = 0,
            WeightUnit = ""
        };

        public static List<Models.Product> Basic = new List<Models.Product>
        {
            EmptyProduct
        };
    }
}