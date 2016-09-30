using BausCode.Api.Models.Attributes;

namespace BausCode.Api.Models.Dto
{
    public class Image
    {
        [Whitelist]
        public int Id { get; set; }

        public ulong Version { get; set; }

        [Whitelist]
        public string Source { get; set; }

        public static Image From(ProductImage source)
        {
            return new Image
            {
                Id = source.Id,
                Version = source.RowVersion,
                Source = source.SourceUrl
            };
        }
    }
}