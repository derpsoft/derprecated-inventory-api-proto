namespace Derprecated.Api.Models.Dto
{
    using Attributes;

    public class Image
    {
        [Whitelist]
        public int Id { get; set; }

        [Whitelist]
        public string Source { get; set; }

        public ulong Version { get; set; }

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
