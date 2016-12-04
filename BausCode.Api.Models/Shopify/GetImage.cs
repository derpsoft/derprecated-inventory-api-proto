namespace BausCode.Api.Models.Shopify
{
    using ServiceStack;

    [Route("/admin/products/{ProductId}/images/{Id}.json", "GET")]
    public class GetImage : IReturn<ImageResponse>
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
    }
}
