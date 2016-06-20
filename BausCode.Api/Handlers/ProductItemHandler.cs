using System.Collections.Generic;
using System.Data;
using BausCode.Api.Models;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    internal class ProductItemHandler
    {
        public ProductItemHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public List<ProductItem> GetProductItems(Product product)
        {
            return Db.Select(Db.From<ProductItem>()
                .Where(pi => pi.ProductId == product.Id));
        }

        public List<ProductItem> GetItemProducts(Item item)
        {
            return Db.Select(Db.From<ProductItem>()
                .Where(pi => pi.ItemId == item.Id));
        }

        public List<int> GetProductIds(Item item)
        {
            return Db.Select<int>(Db.From<ProductItem>()
                .Select(pi => pi.ProductId)
                .Where(pi => pi.ItemId == item.Id));
        }

        public List<int> GetItemIds(Product product)
        {
            return Db.Select<int>(Db.From<ProductItem>()
                .Select(pi => pi.ItemId)
                .Where(pi => pi.ProductId == product.Id));
        }
    }
}