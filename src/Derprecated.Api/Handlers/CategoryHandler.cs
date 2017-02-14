namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack;
    using ServiceStack.OrmLite;

    public class CategoryHandler
    {
        public CategoryHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Category Get(int id, bool includeDeleted = false)
        {
            id.ThrowIfLessThan(1);

            var query = Db.From<Category>()
                .Where(x => x.Id == id);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query)
                .First();
        }

        public long Count(bool includeDeleted = false)
        {
            if (includeDeleted)
                return Db.Count<Category>();

            return Db.Count<Category>(x => !x.IsDeleted);
        }

        public List<Category> List(int skip = 0, int take = 25, bool includeDeleted = false)
        {
            skip.ThrowIfLessThan(0);
            take.ThrowIfLessThan(1);

            var query = Db.From<Category>()
                .Skip(skip)
                .Take(take);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query);
        }

        public List<Category> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Category>()
                .Where(x => x.Name.Contains(q));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.SelectDistinct());
        }

        public Category Save(Category category)
        {
            category.ThrowIfNull();
            if (category.Id >= 1)
            {
                var existing = Get(category.Id);
                if (default(Category) == existing)
                    throw new ArgumentException("invalid Id for existing category", nameof(category));

                category = existing.PopulateWith(category);
            }
            Db.Save(category);

            return category;
        }

        public Category Delete(int id)
        {
            var existing = Get(id);
            if (default(Category) == existing)
                throw new ArgumentException("unable to locate category with id");
            if (existing.IsDeleted)
                throw new Exception("that category was already deleted");
            return Db.SoftDelete(existing);
        }
    }
}
