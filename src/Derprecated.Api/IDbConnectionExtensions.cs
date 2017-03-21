using System;
using System.Data;
using Derprecated.Api.Models;
using ServiceStack.OrmLite;

namespace Derprecated.Api
{
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// Mark this record as deleted, update the deleteDate, and save the record.
        /// Other modifications to record are included in the save.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if the record is currently deleted</exception>
        public static T SoftDelete<T>(this IDbConnection source, T record)
            where T : ISoftDeletable
        {
            if (record.IsDeleted)
                throw new ArgumentException("already deleted", nameof(record));

            record.IsDeleted = true;
            record.DeleteDate = DateTime.UtcNow;
            source.Update(record);
            return record;
        }

        /// <summary>
        /// Mark this record as not deleted and save.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if the record is not currently deleted</exception>
        public static T Restore<T>(this IDbConnection source, T record)
            where T : ISoftDeletable
        {
            if (!record.IsDeleted)
                throw new ArgumentException("has not been deleted", nameof(record));

            record.IsDeleted = false;
            source.Update(record);
            return record;
        }
    }
}
