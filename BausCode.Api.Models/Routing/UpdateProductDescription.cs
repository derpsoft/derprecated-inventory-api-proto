using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}/description", "PUT,POST,PATCH")]
    public class UpdateProductDescription : IUpdatableField<string>, IReturn<ProductResponse>
    {
        public int Id { get; set; }
        public string FieldName => "Description";
        public string Value { get; set; }
    }
}