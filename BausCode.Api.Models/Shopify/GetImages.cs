namespace BausCode.Api.Models.Shopify
{
    using ServiceStack;

    [Route("/admin/products/{ProductId}/images.json", "GET")]
    public class GetImages : IReturn<ImagesResponse>
    {
        public long ProductId { get; set; }
    }
}
