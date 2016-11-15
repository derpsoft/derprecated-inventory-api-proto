using System;
using System.Collections.Generic;
using System.Linq;
using BausCode.Api.Models.Attributes;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class Product : IAuditable
    {
        public Product()
        {
            Images = new List<ProductImage>();
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public long? ShopifyId { get; set; }

        public long? ShopifyVariantId { get; set; }

        [Whitelist]
        public string Title { get; set; }

        [Whitelist]
        public string Description { get; set; }

        [Whitelist]
        public string Tags { get; set; }

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

        [Reference]
        public List<ProductImage> Images { get; set; }


        /// <summary>
        ///     Merge fields from source into this.
        /// </summary>
        /// <param name="source"></param>
        public void Merge(Shopify.Product source)
        {
            Title = source.Title;
            Description = source.BodyHtml;
            Tags = source.Tags;

            foreach (var img in source.Images)
            {
                var i = Images.FirstOrDefault(x => x.ShopifyId == img.Id);

                if (default(ProductImage) == i)
                {
                    Images.Add(ProductImage.From(img));
                }
                else
                {
                    i.SourceUrl = img.Url;
                }
            }
        }

        public void Merge(Product source)
        {
            Title = source.Title;
            Description = source.Description;
            Tags = source.Tags;

            foreach (var img in source.Images)
            {
                var i = Images.FirstOrDefault(x => x.ShopifyId == img.Id);

                if (default(ProductImage) == i)
                {
                    Images.Add(ProductImage.From(img));
                }
                else
                {
                    i.SourceUrl = img.SourceUrl;
                }
            }
        }

        public static Product From(Shopify.Product source)
        {
            var dest = new Product {ShopifyId = source.Id};

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