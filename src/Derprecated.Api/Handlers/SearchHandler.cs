namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using ServiceStack.Auth;
    using ServiceStack.OrmLite;

    public class SearchHandler
    {
        public SearchHandler(IDbConnection db, IAuthSession user)
        {
            Db = db;
        }

        private IDbConnection Db { get; }

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

        public List<Sale> SaleTypeahead(string q)
        {
            return Db.Select(
                Db.From<Sale>()
                );
        }
    }
}
