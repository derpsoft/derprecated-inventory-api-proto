namespace Derprecated.Api.Services
{
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;

    public abstract class CrudService<TModel, TDto> : BaseService
        where TModel : class, IAuditable, ISoftDeletable, IPrimaryKeyable
        where TDto : class, IPrimaryKeyable
    {
        protected static ILog Log = LogManager.GetLogger(typeof(TModel));

        protected CrudService(IHandler<TModel> handler)
        {
            Handler = handler;
        }

        protected IHandler<TModel> Handler { get; }

        public Dto<TDto> Get(TDto request)
        {
            var resp = new Dto<TDto>();
            resp.Result = Handler.Get(request.Id).ConvertTo<TDto>();
            return resp;
        }

        public Dto<TDto> Delete(TDto request)
        {
            var resp = new Dto<TDto>();
            resp.Result = Handler.Delete(request.Id).ConvertTo<TDto>();
            return resp;
        }
    }
}
