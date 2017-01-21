namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
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

        public Category Get(int id)
        {
            id.ThrowIfLessThan(1);

            return Db.SingleById<Category>(id);
        }

        public long Count()
        {
            return Db.Count<Category>();
        }

        public List<Category> List(int skip = 0, int take = 25)
        {
            return Db.Select(
                Db.From<Category>()
                  .Skip(skip)
                  .Take(take)
                );
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
    }
}
