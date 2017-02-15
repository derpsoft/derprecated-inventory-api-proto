﻿namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using Models;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    public class VendorHandler : CrudHandler<Vendor>
    {
        public VendorHandler(IDbConnectionFactory db)
            : base(db)
        {
        }

        public List<Vendor> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Vendor>()
                .Where(x => x.Name.Contains(q));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
        }
    }
}
