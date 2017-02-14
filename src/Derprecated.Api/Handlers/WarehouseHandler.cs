namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using ServiceStack;
    using ServiceStack.OrmLite;
    using System.Linq;

    public class WarehouseHandler
    {
        public WarehouseHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Warehouse Get(int id, bool includeDeleted = false)
        {
            id.ThrowIfLessThan(1);

            var query = Db.From<Warehouse>()
                .Where(x => x.Id == id);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query)
                .First();
        }

        public long Count(bool includeDeleted = false)
        {
            if (includeDeleted)
                return Db.Count<Warehouse>();

            return Db.Count<Warehouse>(x => !x.IsDeleted);
        }

        public List<Warehouse> List(int skip = 0, int take = 25, bool includeDeleted = false)
        {
            skip.ThrowIfLessThan(0);
            take.ThrowIfLessThan(1);

            var query = Db.From<Warehouse>()
                .Skip(skip)
                .Take(take);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query);

        }

        public List<Warehouse> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Warehouse>()
                .Where(x => x.Name.Contains(q));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
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

        public Warehouse Delete(int id)
        {
            var existing = Get(id);
            if (default(Warehouse) == existing)
                throw new ArgumentException("unable to locate warehouse with id");
            if (existing.IsDeleted)
                throw new Exception("that warehouse was already deleted");
            return Db.SoftDelete(existing);
        }
    }
}
