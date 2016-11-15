using System.Data;
using System.Linq;
using BausCode.Api.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    internal class PriceHandler
    {
        public PriceHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public decimal GetPrice(Product product)
        {
            var userId = User.UserAuthId.ToInt();
            var price = Db.Select<decimal>(Db.From<UserPriceOverride>()
                .Select(x => x.Price)
                .Where(x => x.UserAuthId == userId)
                .And(x => x.ProductId == product.Id)
                .Limit(1)
                );

            if (price.Count > 0)
            {
                return price.DefaultIfEmpty(product.Price).FirstOrDefault();
            }

            /* 
             * If no price override found, return default.
             */
            return product.Price;
        }
    }
}