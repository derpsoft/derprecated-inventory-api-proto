namespace Derprecated.Api.Models.Configuration
{
    public sealed class ApplicationConfiguration
    {
        public Auth0 Auth0 { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Mail Mail { get; set; }
        public ServiceStack ServiceStack { get; set; }
        public Shopify Shopify { get; set; }
        public Storage Storage { get; set; }
        public Stripe Stripe {get; set;}
        public Web Web { get; set; }
        public App App { get; set; }
    }
}
