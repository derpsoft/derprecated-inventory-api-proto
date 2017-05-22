namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/categories", "POST")]
    [Route("/api/v1/categories/{Id}", "GET, PUT, PATCH, DELETE")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything,
         Permissions.CanManageCategories,
         Permissions.CanReadCategories)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything,
         Permissions.CanManageCategories,
         Permissions.CanDeleteCategories)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Patch | ApplyTo.Put, Permissions.CanDoEverything,
         Permissions.CanManageCategories,
         Permissions.CanUpsertCategories)]
    public sealed class Category : IReturn<Dto<Category>>, IPrimaryKeyable
    {
        public int Id { get; set; }
        public bool IncludeDeleted { get; set; } = false;
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ulong RowVersion { get; set; }

        public static Category From(Models.Category source)
        {
            return new Category().PopulateWith(source);
        }
    }

    [Route("/api/v1/categories/count", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageCategories,
         Permissions.CanReadCategories)]
    public sealed class CategoryCount : IReturn<Dto<long>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    [Route("/api/v1/categories", "GET")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageCategories,
         Permissions.CanReadCategories)]
    public sealed class Categories : IReturn<Dto<List<Category>>>
    {
        public bool IncludeDeleted { get; set; } = false;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/categories", "GET, SEARCH")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(ApplyTo.Search, Permissions.CanDoEverything, Permissions.CanManageCategories,
         Permissions.CanReadCategories)]
    public sealed class CategorySearch : QueryDb<Models.Category, Category>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }

        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Name",
             ValueFormat = "%{0}%")]
        public string Name { get; set; }
    }

    [Route("/api/v1/categories/typeahead", "GET, SEARCH")]
    [Authenticate]
    [RequiredPermission(Permissions.CanLogin)]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageCategories, Permissions.CanReadCategories)]
    public sealed class CategoryTypeahead : IReturn<Dto<List<Category>>>
    {
        public bool IncludeDeleted { get; set; } = false;

        [StringLength(20)]
        public string Query { get; set; }
    }
}
