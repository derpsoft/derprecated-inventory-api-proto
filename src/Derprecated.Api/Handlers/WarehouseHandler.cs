namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using ServiceStack.OrmLite;

    public class WarehouseHandler : CrudHandler<Warehouse>
    {
        public WarehouseHandler(IDbConnection db)
            : base(db)
        {
        }

        public List<Warehouse> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Warehouse>()
                .Where(x => x.Name.Contains(q));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
        }
    }
}
