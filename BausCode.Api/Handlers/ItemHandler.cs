using System.Data;
using System.Linq;
using BausCode.Api.Models;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    internal class ItemHandler
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public IDbConnection Db { get; set; }

        public decimal QuantityOnHand(int itemId)
        {
            return Db.Select<decimal>(
                Db.From<InventoryTransaction>()
                    .Select(it => it.Quantity)
                    .Where(it => it.ItemId == itemId)
                ).Sum();
        }
    }
}