namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models.Dto;
    using ServiceStack;

    public class CategoryService : BaseService
    {
        public object Get(Category request)
        {
            var resp = new Dto<Category>();
            var handler = new CategoryHandler(Db, CurrentSession);

            resp.Result = Category.From(handler.Get(request.Id, request.IncludeDeleted));

            return resp;
        }

        public object Delete(Category request)
        {
            var resp = new Dto<Category>();
            var handler = new CategoryHandler(Db, CurrentSession);

            resp.Result = Category.From(handler.Delete(request.Id));

            return resp;
        }

        public object Any(Category request)
        {
            var resp = new Dto<Category>();
            var handler = new CategoryHandler(Db, CurrentSession);
            var category = handler.Save(new Models.Category().PopulateWith(request));

            resp.Result = Category.From(category);
            return resp;
        }

        public object Any(CategoryCount request)
        {
            var resp = new Dto<long>();
            var handler = new CategoryHandler(Db, CurrentSession);

            resp.Result = handler.Count(request.IncludeDeleted);

            return resp;
        }

        public object Any(Categories request)
        {
            var resp = new Dto<List<Category>>();
            var handler = new CategoryHandler(Db, CurrentSession);

            resp.Result = handler.List(request.Skip, request.Take, request.IncludeDeleted).Map(Category.From);

            return resp;
        }

        public object Any(CategoryTypeahead request)
        {
            var resp = new Dto<List<Category>>();
            var categoryHandler = new CategoryHandler(Db, CurrentSession);

            if (request.Query.IsNullOrEmpty())
                resp.Result = categoryHandler.List(0, int.MaxValue, request.IncludeDeleted).Map(Category.From);
            else
                resp.Result = categoryHandler.Typeahead(request.Query, request.IncludeDeleted).Map(Category.From);

            return resp;
        }
    }
}
