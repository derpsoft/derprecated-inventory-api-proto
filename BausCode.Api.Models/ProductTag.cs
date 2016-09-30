using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    // ReSharper disable once RedundantExplicitParamsArrayCreation
    [CompositeIndex(new[] {"ProductId", "TagId"})]
    public class ProductTag
    {
        [ForeignKey(typeof(Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

        [ForeignKey(typeof(Tag), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int TagId { get; set; }
    }
}