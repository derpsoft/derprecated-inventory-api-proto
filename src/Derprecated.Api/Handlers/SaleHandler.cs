namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using ServiceStack;
    using ServiceStack.OrmLite;

    public class SaleHandler
    {
        public SaleHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Sale Create(Sale sale)
        {
            sale.ThrowIfNull(nameof(sale));
            sale.Timestamp = DateTime.UtcNow;
            sale.UserAuthId = User.UserAuthId.ToInt();
            Db.Save(sale);
            return sale;
        }

        public Sale Get(int id)
        {
            id.ThrowIfLessThan(1);
            return Db.SingleById<Sale>(id);
        }

        public List<Sale> List(int skip = 0, int take = 25)
        {
            skip.ThrowIfLessThan(0);
            take.ThrowIfLessThan(1);

            return Db.Select(
                Db.From<Sale>()
                    .Skip(skip)
                    .Take(take)
            );
        }

        public long Count()
        {
            return Db.Count<Sale>();
        }
    }
}
