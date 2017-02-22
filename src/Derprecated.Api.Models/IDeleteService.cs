namespace Derprecated.Api.Models
{
    using ServiceStack;

    public interface IDeleteService<TModel, TDto> : IDelete<TDto>
        where TModel : class, IPrimaryKeyable, ISoftDeletable
        where TDto : class, IPrimaryKeyable
    {
    }
}
