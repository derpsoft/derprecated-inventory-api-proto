using System.Collections.Generic;
using System.Data;
using System.Linq;
using BausCode.Api.Models;
using ServiceStack;
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

        public List<KeyValuePair<int, decimal>> QuantityOnHand(List<int> itemIds)
        {
            return itemIds.Map(i => new KeyValuePair<int, decimal>(i, QuantityOnHand(i)));
        }

        public List<Item> GetItems(List<int> ids)
        {
            return Db.SelectByIds<Item>(ids);
        }
    }
}