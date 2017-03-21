namespace Derprecated.Api.Models
{
    using System.Collections.Generic;

    public interface IWriteHandler<T>
        where T : class, ISoftDeletable
    {
        T Delete(int id);
        T Save(T record, bool includeReferences = false);
    }
}
