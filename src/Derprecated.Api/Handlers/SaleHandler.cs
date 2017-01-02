namespace Derprecated.Api.Handlers
{
    using System;
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

        public void Create(Sale sale)
        {
            sale.ThrowIfNull(nameof(sale));
            sale.Timestamp = DateTime.UtcNow;
            Db.Save(sale);
        }
    }
}
