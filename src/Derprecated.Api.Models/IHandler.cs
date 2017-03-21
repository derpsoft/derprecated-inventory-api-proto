namespace Derprecated.Api.Models
{
    using System.Collections.Generic;

    public interface IHandler<T> : IReadHandler<T>, IWriteHandler<T>
        where T : class, ISoftDeletable, IPrimaryKeyable
    {
    }
}
