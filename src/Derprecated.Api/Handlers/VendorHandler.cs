namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack;
    using ServiceStack.OrmLite;

    public class VendorHandler
    {
        public VendorHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Vendor Get(int id, bool includeDeleted = false)
        {
            id.ThrowIfLessThan(1);

            var query = Db.From<Vendor>()
                .Where(x => x.Id == id);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query)
                .First();
        }

        public long Count(bool includeDeleted = false)
        {
            if (includeDeleted)
                return Db.Count<Vendor>();

            return Db.Count<Vendor>(x => !x.IsDeleted);

        }

        public List<Vendor> List(int skip = 0, int take = 25, bool includeDeleted = false)
        {
            skip.ThrowIfLessThan(0);
            take.ThrowIfLessThan(1);

            var query = Db.From<Vendor>()
                .Skip(skip)
                .Take(take);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query);
        }

        public List<Vendor> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Vendor>()
                .Where(x => x.Name.Contains(q));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
        }

        public Vendor Save(Vendor vendor)
        {
            vendor.ThrowIfNull();
            if (vendor.Id >= 1)
            {
                var existing = Get(vendor.Id);
                if (default(Vendor) == existing)
                    throw new ArgumentException("invalid Id for existing vendor", nameof(vendor));

                vendor = existing.PopulateWith(vendor);
            }
            Db.Save(vendor);

            return vendor;
        }

        public Vendor Delete(int id)
        {
            var existing = Get(id);
            if (default(Vendor) == existing)
                throw new ArgumentException("unable to locate vendor with id");
            if (existing.IsDeleted)
                throw new Exception("that vendor was already deleted");
            return Db.SoftDelete(existing);
        }
    }
}
