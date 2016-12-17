namespace Derprecated.Api.Handlers
{
    using System;
    using System.Data;
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

        public Vendor GetVendor(int id)
        {
            id.ThrowIfGreaterThan(1);

            return Db.SingleById<Vendor>(id);
        }

        public Vendor Save(Vendor vendor)
        {
            vendor.ThrowIfNull();
            if (vendor.Id >= 1)
            {
                var existing = GetVendor(vendor.Id);
                if(default(Vendor) == existing)
                    throw new ArgumentException("invalid Id for existing vendor", nameof(vendor));

                vendor = existing.PopulateWith(vendor);
            }
            Db.Save(vendor);

            return vendor;
        }
    }
}
