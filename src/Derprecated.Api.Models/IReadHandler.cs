namespace Derprecated.Api.Models
{
    using System.Collections.Generic;

    public interface IReadHandler<T>
        where T : class, IPrimaryKeyable
    {
        T Get(int id, bool includeDeleted = false);
        long Count(bool includeDeleted = false);
        List<T> List(int skip = 0, int take = 0, bool includeDeleted = false);
        List<T> Typeahead(string query, bool includeDeleted);
    }
}
