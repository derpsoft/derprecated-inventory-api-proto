using System.Data;
using System.Linq;
using BausCode.Api.Models;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    internal class ItemHandler
    {
        public ItemHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; set; }

        public Item GetItem(int id)
        {
            id.ThrowIfLessThan(1);
            return Db.SingleById<Item>(id);
        }

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