using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/products/{Id}.json", "GET")]
    public class GetVariant : IReturn<VariantResponse>
    {
        public long Id { get; set; }
    }
}