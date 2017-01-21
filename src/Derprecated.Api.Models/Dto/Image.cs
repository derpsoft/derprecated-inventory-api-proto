namespace Derprecated.Api.Models.Dto
{
    using Attributes;

    public class Image
    {
        [Whitelist]
        public int Id { get; set; }

        [Whitelist]
        public string Source { get; set; }

        public static Image From(Models.ProductImage source)
        {
            return new Image
            {
                Id = source.Id,
                Source = source.SourceUrl
            };
        }
    }
}
