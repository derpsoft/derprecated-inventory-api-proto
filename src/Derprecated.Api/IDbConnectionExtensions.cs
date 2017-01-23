namespace Derprecated.Api
{
    using System;
    using System.Data;
    using Models;
    using ServiceStack.OrmLite;

    public static class DbConnectionExtensions
    {
        public static T SoftDelete<T>(this IDbConnection source, T record)
            where T: ISoftDeletable
        {
            record.IsDeleted = true;
            record.DeleteDate = DateTime.UtcNow;
            source.Update(record);
            return record;
        }
    }
}
