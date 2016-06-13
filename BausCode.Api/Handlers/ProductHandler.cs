﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using BausCode.Api.Models;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    internal class ProductHandler
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public IDbConnection Db { get; set; }

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
    }
}