namespace Derprecated.Api.Models
{
    using ServiceStack;

    public interface IUpdateService<TModel, TDto> : IPut<TDto>, IPatch<TDto>
        where TModel : class, IPrimaryKeyable, ISoftDeletable where TDto : class, IPrimaryKeyable
    {
    }
}
