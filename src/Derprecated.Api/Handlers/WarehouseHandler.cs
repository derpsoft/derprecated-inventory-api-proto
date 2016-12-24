namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using ServiceStack;
    using ServiceStack.OrmLite;

    public class WarehouseHandler
    {
        public WarehouseHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Warehouse Get(int id)
        {
            id.ThrowIfLessThan(1);

            return Db.SingleById<Warehouse>(id);
        }

        public long Count()
        {
            return Db.Count<Warehouse>();
        }

        public List<Warehouse> List(int skip = 0, int take = 25)
        {
            return Db.Select(
                Db.From<Warehouse>()
                  .Skip(skip)
                  .Take(take)
                );
        }

        public Warehouse Save(Warehouse warehouse)
        {
            warehouse.ThrowIfNull();
            if (warehouse.Id >= 1)
            {
                var existing = Get(warehouse.Id);
                if (default(Warehouse) == existing)
                    throw new ArgumentException("invalid Id for existing warehouse", nameof(warehouse));

                warehouse = existing.PopulateWith(warehouse);
            }
            Db.Save(warehouse);

            return warehouse;
        }
    }
}
