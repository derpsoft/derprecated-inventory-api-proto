namespace Derprecated.Api.Handlers
{
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.OrmLite;

    internal class PriceHandler
    {
        public PriceHandler(IDbConnection db, IAuthSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private IAuthSession User { get; }

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
