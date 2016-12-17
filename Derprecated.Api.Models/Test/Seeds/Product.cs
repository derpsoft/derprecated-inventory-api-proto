namespace Derprecated.Api.Models.Test.Seeds
{
    using System.Collections.Generic;
    using System.Drawing;

    public static class Product
    {
        public static readonly Models.Product EmptyProduct = new Models.Product
                                                             {
                                                                 Id = 1,
                                                                 Barcode = "",
                                                                 Color = Color.Transparent.Name,
                                                                 Description = "",
                                                                 Grams = 0,
                                                                 Images = new List<ProductImage>(),
                                                                 Price = 0m,
                                                                 RowVersion = 1,
                                                                 Tags = "",
                                                                 Title = "",
                                                                 Sku = "",
                                                                 Weight = 0,
                                                                 WeightUnit = ""
                                                             };

        public static readonly Models.Product Light = new Models.Product
                                                      {
                                                          Id = 2,
                                                          Barcode = "LIGHT2",
                                                          Color = Color.Blue.Name,
                                                          Description = "Test prouduct, blue light"
                                                      };

        public static readonly Models.Product CatTree = new Models.Product
                                                      {
                                                          Id = 3,
                                                          Barcode = "CATTREE3",
                                                          Color = Color.Brown.Name,
                                                          Description = "Test prouduct, cat tree"
                                                      };

        public static List<Models.Product> Basic = new List<Models.Product>
                                                   {
                                                       EmptyProduct
                                                   };
    }
}
