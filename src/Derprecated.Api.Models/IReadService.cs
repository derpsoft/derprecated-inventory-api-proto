namespace Derprecated.Api.Models
{
    using ServiceStack;

    public interface IReadService<TModel, TDto> : IGet<TDto>
        where TModel : class, IPrimaryKeyable, ISoftDeletable
        where TDto : class, IPrimaryKeyable
    {
    }
}
