using BausCode.Api.Models.Attributes;

namespace BausCode.Api.Models
{
    public class ProductMeta
    {
        [Whitelist]
        public string Title { get; set; }

        [Whitelist]
        public string Description { get; set; }

        [Whitelist]
        public string Tags { get; set; }
    }
}