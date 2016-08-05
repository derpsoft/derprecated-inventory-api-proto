using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private UserSession User { get; }

        public Product GetProduct(int id)
        {
            id.ThrowIfLessThan(1);
            return Db.LoadSingleById<Product>(id);
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
        public List<Product> GetProducts(int? skip, int? take)
        {
            return Db.LoadSelect(Db.From<Product>().Skip(skip).Take(take));
        }

        /// <summary>
        ///     Get quantity on hand for a particular updatedProduct.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        ///     Wraps ItemHandler#GetQuantityOnHand(1)
        /// </remarks>
        public decimal GetQuantityOnHand(int id)
        {
            throw new NotImplementedException();

            var product = GetProduct(id);
            var itemHandler = new ItemHandler(Db, User);
            var idsHandler = new ProductItemHandler(Db, User);

            // TODO(jamesearl)
            // How do we decide how many products are on hand, given the number of items?
            //
            var itemsOnHand = itemHandler.QuantityOnHand(idsHandler.GetItemIds(product));
        }

        /// <summary>
        /// Update an existing Product.
        /// </summary>
        /// <param name="id">The ID of the Product to update.</param>
        /// <param name="updatedProduct">The values to update the existing Product with.</param>
        /// <returns></returns>
        public Product Update(int id, Product updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}