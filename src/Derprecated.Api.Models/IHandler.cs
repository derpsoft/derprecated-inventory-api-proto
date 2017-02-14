namespace Derprecated.Api.Models
{
    public interface IHandler<T>
        where T : class, ISoftDeletable
    {
        T Get(int id, bool includeDeleted = false);
        T Delete(int id);
    }
}
