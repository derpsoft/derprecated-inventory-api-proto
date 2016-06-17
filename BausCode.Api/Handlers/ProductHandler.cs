using System.Collections.Generic;
using System.Data;
using BausCode.Api.Models;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    internal class ProductHandler
    {
        public ProductHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; set; }

        public Product GetProduct(int id)
        {
            id.ThrowIfLessThan(1);
            return Db.SingleById<Product>(id);
        }

        /// <summary>
        ///     Get all products.
        /// </summary>
        /// <returns>
        ///     A list of Product elements.
        /// </returns>
        /// <remarks>
        ///     Might be slow, use with caution.
        /// </remarks>
        public List<Product> GetProducts()
        {
            return Db.Select<Product>();
        }

        /// <summary>
        /// Get quantity on hand for a particular product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Wraps ItemHandler#GetQuantityOnHand(1)
        /// </remarks>
        public decimal GetQuantityOnHand(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}