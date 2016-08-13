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

        [Reference]
        public List<ProductVariant> Variants { get; set; }

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