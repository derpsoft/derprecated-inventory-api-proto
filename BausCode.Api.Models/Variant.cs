using System;
using BausCode.Api.Models.Attributes;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class Variant : IAuditable    
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public long ShopifyId { get; set; }
        public long ProductShopifyId { get; set; }

        [ForeignKey(typeof (Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

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

        [Whitelist]
        public string Color { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }

        /// <summary>
        ///     Merge fields from source into this.
        /// </summary>
        /// <param name="source"></param>
        public void Merge(Shopify.Variant source)
        {
            Title = source.Title;
            Price = decimal.Parse(source.Price);
            Sku = source.Sku;
            Grams = source.Grams;
            Barcode = source.Barcode;
            Weight = source.Weight;
            WeightUnit = source.WeightUnit;
        }

        public static Variant From(Shopify.Variant source)
        {
            var dest = new Variant {ShopifyId = source.Id, ProductShopifyId = source.ProductShopifyId};

            dest.Merge(source);

            return dest;
        }

        public void OnInsert()
        {
            OnUpsert();
        }

        public void OnUpdate()
        {
            OnUpsert();
        }

        private void OnUpsert()
        {
        }
    }
}