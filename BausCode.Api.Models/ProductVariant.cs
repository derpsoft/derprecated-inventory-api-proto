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

        [ForeignKey(typeof(Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

        public ProductVariantMeta Meta { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }

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