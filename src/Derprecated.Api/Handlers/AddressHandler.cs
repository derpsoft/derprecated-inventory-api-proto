namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using Models;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    public class AddressHandler : CrudHandler<Address>
    {
        public AddressHandler(IDbConnectionFactory db)
            : base(db)
        {
        }

        public override List<Address> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Address>()
                .Where(x => x.Line1.Contains(q))
                .Or(x => x.Line2.Contains(q))
                .Or(x => x.City.Contains(q))
                .Or(x => x.State.Contains(q))
                .Or(x => x.Zip.Contains(q))
                ;

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
        }
    }
}
