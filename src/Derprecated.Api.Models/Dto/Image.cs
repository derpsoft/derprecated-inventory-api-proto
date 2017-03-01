namespace Derprecated.Api.Models.Dto
{
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/images", "POST")]
    [Route("/api/v1/images/{Id}", "GET, PUT, PATCH, DELETE")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything,
         Permissions.CanManageImages,
         Permissions.CanReadImages)]
    [RequiresAnyPermission(ApplyTo.Delete, Permissions.CanDoEverything,
         Permissions.CanManageImages,
         Permissions.CanDeleteImages)]
    [RequiresAnyPermission(ApplyTo.Post | ApplyTo.Patch | ApplyTo.Put, Permissions.CanDoEverything,
         Permissions.CanManageImages,
         Permissions.CanUpsertImages)]
    public class Image : IReturn<Dto<Image>>, IPrimaryKeyable
    {
        public int Id { get; set; }
        public List<int> ProductIds { get; set; }
        public List<Product> Products { get; set; }
        public ulong RowVersion { get; set; }
        public string Url { get; set; }
        public bool IncludeDeleted { get; set; } = false;
    }

    [Route("/api/v1/images/count", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageImages,
         Permissions.CanReadImages)]
    public sealed class ImageCount : IReturn<Dto<long>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    [Route("/api/v1/images", "GET")]
    [Authenticate]
    [RequiresAnyPermission(ApplyTo.Get, Permissions.CanDoEverything, Permissions.CanManageImages,
         Permissions.CanReadImages)]
    public sealed class Images : IReturn<Dto<List<Image>>>
    {
        public bool IncludeDeleted { get; set; } = false;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }

    [Route("/api/v1/images/typeahead", "GET, SEARCH")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageImages, Permissions.CanReadImages)]
    public sealed class ImageTypeahead : IReturn<Dto<List<Image>>>
    {
        public bool IncludeDeleted { get; set; } = false;

        [StringLength(20)]
        public string Query { get; set; }
    }

}
