namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/categories", "POST")]
    [Route("/api/v1/categories/{Id}", "GET, PUT, PATCH, DELETE")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything,
        Permissions.CanManageCategories,
        Permissions.CanReadCategories)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything,
        Permissions.CanManageCategories,
        Permissions.CanDeleteCategories)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Patch | ApplyTo.Put, Permissions.CanDoEverything,
        Permissions.CanManageCategories,
        Permissions.CanUpsertCategories)]
    public sealed class Category : IReturn<Dto<Category>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ulong RowVersion { get; set; }
        public int ParentId { get; set; }

        public static Category From(Models.Category source)
        {
            return new Category().PopulateWith(source);
        }
    }

    [Route("/api/v1/categories/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageCategories,
        Permissions.CanReadCategories)]
    public sealed class CategoryCount : IReturn<Dto<long>>
    {
    }

    [Route("/api/v1/categories", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageCategories,
        Permissions.CanReadCategories)]
    public sealed class Categories : IReturn<Dto<List<Category>>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/categories", "SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Search, Permissions.CanDoEverything, Permissions.CanManageVendors,
        Permissions.CanReadVendors)]
    public sealed class CategorySearch : QueryDb<Models.Category, Category>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }

        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Name",
            ValueFormat = "%{0}%")]
        public string Name { get; set; }
    }

    [Route("/api/v1/categories/typeahead", "SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageCategories, Permissions.CanReadCategories)]
    public sealed class CategoryTypeahead : IReturn<Dto<List<Category>>>
    {
        [StringLength(20)]
        public string Query { get; set; }
    }
}
