namespace Derprecated.Api.Services
{
    using System.Collections.Generic;
    using Models;
    using Models.Dto;
    using ServiceStack;
    using ServiceStack.Logging;
    using Category = Models.Category;

    public static class CategoryServices
    {
        private static ILog Log = LogManager.GetLogger(typeof(CategoryServices));

        public class CategoryService : CrudService<Category, Models.Dto.Category>
        {
            public CategoryService(IHandler<Category> handler) : base(handler)
            {
            }

            public object Get(CategoryCount request)
            {
                var resp = new Dto<long>();

                resp.Result = Handler.Count(request.IncludeDeleted);

                return resp;
            }

            public object Get(Categories request)
            {
                var resp = new Dto<List<Models.Dto.Category>>();

                resp.Result =
                    Handler.List(request.Skip, request.Take, request.IncludeDeleted).Map(Models.Dto.Category.From);

                return resp;
            }

            public object Any(CategoryTypeahead request)
            {
                var resp = new Dto<List<Models.Dto.Category>>();

                if (request.Query.IsNullOrEmpty())
                    resp.Result = Handler.List(0, int.MaxValue, request.IncludeDeleted).Map(Models.Dto.Category.From);
                else
                    resp.Result = Handler.Typeahead(request.Query, request.IncludeDeleted).Map(Models.Dto.Category.From);

                return resp;
            }
        }
    }
}
