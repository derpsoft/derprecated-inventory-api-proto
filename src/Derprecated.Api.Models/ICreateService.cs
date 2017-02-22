namespace Derprecated.Api.Models
{
    using ServiceStack;

    public interface ICreateService<TModel, TDto> : IPost<TDto>
        where TModel : class, ISoftDeletable
        where TDto : class
    {
    }
}
