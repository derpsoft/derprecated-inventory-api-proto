namespace Derprecated.Api.Models
{
    using System.Collections.Generic;

    public interface IHandler<T>
        where T : class, ISoftDeletable
    {
        T Get(int id, bool includeDeleted = false);
        T Delete(int id);
        long Count(bool includeDeleted = false);
        T Save(T record, bool includeReferences = false);
        List<T> List(int skip = 0, int take = 0, bool includeDeleted = false);
        List<T> Typeahead(string query, bool includeDeleted = false);
    }
}
