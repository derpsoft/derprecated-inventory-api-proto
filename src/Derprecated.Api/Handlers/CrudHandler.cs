namespace Derprecated.Api.Handlers
{
    using System;
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack.OrmLite;

    public abstract class CrudHandler<T> : IHandler<T>
        where T : class, ISoftDeletable, IPrimaryKeyable
    {
        protected CrudHandler(IDbConnection db)
        {
            Db = db;
        }

        protected IDbConnection Db { get; }

        public T Delete(int id)
        {
            var existing = Get(id);
            if (default(T) == existing)
                throw new ArgumentException("unable to find a record with that id", nameof(id));

            return Db.SoftDelete(existing);
        }

        public T Get(int id, bool includeDeleted = false)
        {
            id.ThrowIfLessThan(1);

            var query = Db.From<T>()
                .Where(x => x.Id == id);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query)
                .First();
        }
    }
}
