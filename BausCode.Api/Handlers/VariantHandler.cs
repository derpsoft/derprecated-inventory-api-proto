using System.Collections.Generic;
using System.Data;
using System.Linq;
using BausCode.Api.Models;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    internal class VariantHandler
    {
        public VariantHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; set; }

        public Variant GetVariant(int id)
        {
            id.ThrowIfLessThan(1);

            var variant = Db.SingleById<Variant>(id);

            variant.Price = new PriceHandler(Db, User).GetPrice(variant);

            return variant;
        }

        public decimal QuantityOnHand(int variantId)
        {
            return Db.Select<decimal>(
                Db.From<InventoryTransaction>()
                    .Where(it => it.ProductVariantId == variantId)
                    .Select(it => it.Quantity)
                ).Sum();
        }

        public Dictionary<int, decimal> QuantityOnHand(IEnumerable<int> variantIds)
        {
            return Db.Dictionary<int, decimal>(
                Db.From<InventoryTransaction>()
                    .Where(it => Sql.In(it.ProductVariantId, variantIds))
                    .GroupBy(it => it.ProductId)
                    .Select(it => new {it.ProductVariantId, Quantity = Sql.Sum(it.Quantity)})
                );
        }
    }
}