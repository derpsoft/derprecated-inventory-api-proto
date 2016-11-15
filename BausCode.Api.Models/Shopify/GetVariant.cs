using System.Runtime.Serialization;
using ServiceStack;

namespace BausCode.Api.Models.Shopify
{
    [Route("/admin/variants/{Id}.json", "GET")]
    public class GetVariant : IReturn<VariantResponse>
    {
        public long Id { get; set; }
    }
}