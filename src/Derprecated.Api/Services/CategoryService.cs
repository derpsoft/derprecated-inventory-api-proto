namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Handlers;
    using Models.Dto;
    using ServiceStack;

    public class CategoryService : BaseService
    {
        public CategoryHandler Handler { get; set; }

        public object Get(Category request)
        {
            var resp = new Dto<Category>();

            resp.Result = Category.From(Handler.Get(request.Id, request.IncludeDeleted));

            return resp;
        }

        public object Delete(Category request)
        {
            var resp = new Dto<Category>();

            resp.Result = Category.From(Handler.Delete(request.Id));

            return resp;
        }

        public object Any(Category request)
        {
            var resp = new Dto<Category>();
            var category = Handler.Save(new Models.Category().PopulateWith(request));

            resp.Result = Category.From(category);
            return resp;
        }

        public object Any(CategoryCount request)
        {
            var resp = new Dto<long>();

            resp.Result = Handler.Count(request.IncludeDeleted);

            return resp;
        }

        public object Any(Categories request)
        {
            var resp = new Dto<List<Category>>();

            resp.Result = Handler.List(request.Skip, request.Take, request.IncludeDeleted).Map(Category.From);

            return resp;
        }

        public object Any(CategoryTypeahead request)
        {
            var resp = new Dto<List<Category>>();

            if (request.Query.IsNullOrEmpty())
                resp.Result = Handler.List(0, int.MaxValue, request.IncludeDeleted).Map(Category.From);
            else
                resp.Result = Handler.Typeahead(request.Query, request.IncludeDeleted).Map(Category.From);

            return resp;
        }
    }
}
