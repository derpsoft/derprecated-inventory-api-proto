namespace BausCode.Api.Handlers
{
    using System.Data;
    using Models;
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
    }
}
