using System;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class ProductVariant
    {
        public ProductVariant()
        {
            Meta = new ProductVariantMeta();
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public long ShopifyId { get; set; }
        public long ProductShopifyId { get; set; }

        [ForeignKey(typeof (Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

        public ProductVariantMeta Meta { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }

        /// <summary>
        ///     Merge fields from source into this.
        /// </summary>
        /// <param name="source"></param>
        public void Merge(Dto.Shopify.ProductVariant source)
        {
            Meta.Title = source.Title;
            Meta.Price = decimal.Parse(source.Price);
            Meta.Sku = source.Sku;
            Meta.Grams = source.Grams;
            Meta.Barcode = source.Barcode;
            Meta.Weight = source.Weight;
            Meta.WeightUnit = source.WeightUnit;
        }

        public static ProductVariant From(Dto.Shopify.ProductVariant source)
        {
            var dest = new ProductVariant {ShopifyId = source.Id, ProductShopifyId = source.ProductShopifyId};

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