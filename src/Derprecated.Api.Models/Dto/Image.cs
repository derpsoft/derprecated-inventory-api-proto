namespace Derprecated.Api.Models.Dto
{
    using Attributes;
    using ServiceStack;

    public class Image
    {
        [Whitelist]
        public int Id { get; set; }

        [Whitelist]
        public string SourceUrl { get; set; }

        public static Image From(Models.ProductImage source)
        {
            return new Image().PopulateWith(source);
        }
    }
}
