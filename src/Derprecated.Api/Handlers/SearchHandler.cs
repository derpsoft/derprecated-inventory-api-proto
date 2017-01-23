namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using ServiceStack.Auth;
    using ServiceStack.OrmLite;

    public class SearchHandler
    {
        public SearchHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public List<UserAuth> UserTypeahead(string q)
        {
            return Db.Select(
                Db.From<UserAuth>()
                  .Where(x => x.FirstName.Contains(q))
                  .Or(x => x.UserName.Contains(q))
                  .Or(x => x.LastName.Contains(q))
                  .Or(x => x.PhoneNumber.Contains(q))
                  .Or(x => x.Email.Contains(q))
                  .SelectDistinct()
                );
        }

        public List<Product> ProductTypeahead(string q)
        {
            return Db.LoadSelect(
                Db.From<Product>()
                  .Where(x => x.Title.Contains(q))
                  .Or(x => x.Sku.Contains(q))
                  .SelectDistinct()
                );
        }

        public List<Vendor> VendorTypeahead(string q)
        {
            return Db.Select(
                Db.From<Vendor>()
                  .Where(x => x.Name.Contains(q))
                  .SelectDistinct()
                );
        }

        public List<Warehouse> WarehouseTypeahead(string q)
        {
            return Db.Select(
                Db.From<Warehouse>()
                  .Where(x => x.Name.Contains(q))
                  .SelectDistinct()
                );
        }

        public List<Category> CategoryTypeahead(string q)
        {
            return Db.Select(
                Db.From<Category>()
                    .Where(x => x.Name.Contains(q))
                    .SelectDistinct()
                );
        }

        public List<Location> LocationTypeahead(string q)
        {
            return Db.Select(
                Db.From<Location>()
                    .Where(x => x.Bin.Contains(q))
                    .Or(x => x.Rack.Contains(q))
                    .Or(x => x.Shelf.Contains(q))
                );
        }
    }
}
