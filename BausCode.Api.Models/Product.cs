using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class Product : IAuditable
    {
        public Product()
        {
            Meta = new ProductMeta();
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public long ShopifyId { get; set; }

        public ProductMeta Meta { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }


        /// <summary>
        ///     Merge fields from source into this.
        /// </summary>
        /// <param name="source"></param>
        public void Merge(Dto.Shopify.Product source)
        {
            Meta.Title = source.Title;
            Meta.Description = source.BodyHtml;
            Meta.Tags = source.Tags;

            foreach (var pv in source.Variants)
            {
                var v = Variants.FirstOrDefault(x => x.ShopifyId == pv.Id);

                if (default(ProductVariant) == v)
                {
                    Variants.Add(ProductVariant.From(pv));
                }
                else
                {
                    v.Merge(pv);
                }
            }

            foreach (var img in source.Images)
            {
                var i = Images.FirstOrDefault(x => x.ShopifyId == img.Id);

                if (default(ProductImage) == i)
                {
                    Images.Add(ProductImage.From(img));
                }
                else
                {
                    i.Merge(img);
                }
            }
        }

        public static Product From(Dto.Shopify.Product source)
        {
            var dest = new Product { ShopifyId = source.Id };

            dest.Merge(source);

            return dest;
        }
        [Reference]
        public List<ProductVariant> Variants { get; set; }

        [Reference]
        public List<ProductImage>  Images { get; set; }

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