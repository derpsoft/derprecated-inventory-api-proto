namespace Derprecated.Api.Services
{
    using Models;
    using Models.Dto;
    using ServiceStack;

    public abstract class CrudService<TModel, TDto> : BaseService, IGet<TDto>, IPut<TDto>, IPost<TDto>, IPatch<TDto>,
            IDelete<TDto>
        where TModel : class, ISoftDeletable, IPrimaryKeyable
        where TDto : class, IPrimaryKeyable
    {
        protected CrudService(IHandler<TModel> handler)
        {
            Handler = handler;
        }

        protected IHandler<TModel> Handler { get; }

        protected virtual object Create(TDto request)
        {
            var resp = new Dto<TDto>();
            var newRecord = Handler.Save(request.ConvertTo<TModel>());
            resp.Result = newRecord.ConvertTo<TDto>();
            return resp;
        }

        protected virtual object Update(TDto request)
        {
            var resp = new Dto<TDto>();
            var newRecord = Handler.Save(request.ConvertTo<TModel>());
            resp.Result = newRecord.ConvertTo<TDto>();
            return resp;
        }

        public object Delete(TDto request)
        {
            var resp = new Dto<TDto>();
            resp.Result = Handler.Delete(request.Id).ConvertTo<TDto>();
            return resp;
        }

        public virtual object Get(TDto request)
        {
            var resp = new Dto<TDto>();
            resp.Result = Handler.Get(request.Id).ConvertTo<TDto>();
            return resp;
        }

        public virtual object Patch(TDto request)
        {
            return Update(request);
        }

        public virtual object Post(TDto request)
        {
            return Create(request);
        }

        public virtual object Put(TDto request)
        {
            return Update(request);
        }
    }

    public abstract class CreateService<TModel, TDto> : BaseService, ICreateService<TModel, TDto>
        where TModel : class, ISoftDeletable
        where TDto : class
    {
        protected CreateService(IHandler<TModel> handler)
        {
            Handler = handler;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        protected IHandler<TModel> Handler { get; }

        protected virtual object Create(TDto request)
        {
            var resp = new Dto<TDto>();
            var newRecord = Handler.Save(request.ConvertTo<TModel>());
            resp.Result = newRecord.ConvertTo<TDto>();
            return resp;
        }

        public object Post(TDto request)
        {
            return Create(request);
        }
    }

    public abstract class ReadService<TModel, TDto> : BaseService, IReadService<TModel, TDto>
        where TModel : class, IPrimaryKeyable, ISoftDeletable
        where TDto : class, IPrimaryKeyable
    {
        protected ReadService(IHandler<TModel> handler)
        {
            Handler = handler;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        protected IHandler<TModel> Handler { get; }

        public virtual object Get(TDto request)
        {
            var resp = new Dto<TDto>();
            resp.Result = Handler.Get(request.Id).ConvertTo<TDto>();
            return resp;
        }
    }

    public abstract class UpdateService<TModel, TDto> : BaseService, IUpdateService<TModel, TDto>
        where TModel : class, IPrimaryKeyable, ISoftDeletable where TDto : class, IPrimaryKeyable
    {
        protected UpdateService(IHandler<TModel> handler)
        {
            Handler = handler;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        protected IHandler<TModel> Handler { get; }

        private object Update(TDto request)
        {
            var resp = new Dto<TDto>();
            var newRecord = Handler.Save(request.ConvertTo<TModel>());
            resp.Result = newRecord.ConvertTo<TDto>();
            return resp;
        }

        public object Patch(TDto request)
        {
            return Update(request);
        }

        public object Put(TDto request)
        {
            return Update(request);
        }
    }

    public abstract class DeleteService<TModel, TDto> : BaseService, IDeleteService<TModel, TDto>
        where TModel : class, IPrimaryKeyable, ISoftDeletable
        where TDto : class, IPrimaryKeyable
    {
        protected DeleteService(IHandler<TModel> handler)
        {
            Handler = handler;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        protected IHandler<TModel> Handler { get; }

        public object Delete(TDto request)
        {
            var resp = new Dto<TDto>();
            resp.Result = Handler.Delete(request.Id).ConvertTo<TDto>();
            return resp;
        }
    }
}
