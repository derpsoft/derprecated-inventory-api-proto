﻿namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    public abstract class CrudHandler<T> : IHandler<T>, IDisposable
        where T : class, ISoftDeletable, IPrimaryKeyable, IAuditable
    {
        protected CrudHandler(IDbConnectionFactory db)
        {
            Db = db.Open();
        }

        protected IDbConnection Db { get; }

        public virtual SqlExpression<T> AddJoinTables(SqlExpression<T> source)
        {
            return source;
        }

        public virtual SqlExpression<T> QueryForGet(int id, bool includeDeleted = false)
        {
            id.ThrowIfLessThan(1);

            var query = AddJoinTables(Db.From<T>())
                .Where(x => x.Id == id);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return query;
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        public long Count(bool includeDeleted = false)
        {
            if (includeDeleted)
                return Db.Count<T>();

            return Db.Count<T>(x => !x.IsDeleted);
        }

        public virtual List<T> List(int skip = 0, int take = 0, bool includeDeleted = false)
        {
            skip.ThrowIfLessThan(0);
            take.ThrowIfLessThan(1);

            var query = AddJoinTables(Db.From<T>())
                          .Skip(skip)
                          .Take(take);

            if (!includeDeleted)
                query = query.Where(x => !x.IsDeleted);

            return Db.LoadSelect(query.OrderByDescending(x => x.ModifyDate));
        }

        public abstract List<T> Typeahead(string query, bool includeDeleted = false);

        protected virtual void BeforeCreate(T record){}

        public virtual T Save(T record, bool includeReferences = false)
        {
            record.ThrowIfNull();

            if (record.Id >= 1)
            {
                var existing = Get(record.Id);
                if (default(Location) == existing)
                    throw new ArgumentException("unable to find a record with that id", nameof(record));

                record = existing.PopulateWith(record);
            }
            else
            {
                BeforeCreate(record);
            }
            Db.Save(record, includeReferences);

            return record;
        }

        protected virtual void BeforeDelete(T record){}

        public virtual T Delete(int id)
        {
            var existing = Get(id);

            if (default(T) == existing)
                throw new ArgumentException("unable to find a record with that id", nameof(id));
            if (existing.IsDeleted)
                throw new Exception("that record was already deleted");

            BeforeDelete(existing);

            return Db.SoftDelete(existing);
        }

        public virtual T Get(int id, bool includeDeleted = false)
        {
            return Db.LoadSelect(QueryForGet(id, includeDeleted))
                     .Single();
        }
    }
}
