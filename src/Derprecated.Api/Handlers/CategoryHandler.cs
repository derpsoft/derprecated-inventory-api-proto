﻿namespace Derprecated.Api.Handlers
{
    using System.Collections.Generic;
    using Models;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    public class CategoryHandler : CrudHandler<Category>
    {
        public CategoryHandler(IDbConnectionFactory db)
            : base(db)
        {
        }

        public override List<Category> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Category>()
                .Where(x => x.Name.Contains(q));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
        }
    }
}
