namespace Derprecated.Api.Models.Configuration
{
    public sealed class ApplicationConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public ServiceStack ServiceStack { get; set; }
        public Mail Mail { get; set; }
        public Storage Storage { get; set; }
        public Shopify Shopify { get; set; }
        public Web Web { get; set; }
    }
}
