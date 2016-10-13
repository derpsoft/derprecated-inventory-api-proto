using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/{Id}/title", "PUT,POST,PATCH")]
    public class UpdateProductTitle : IUpdatableField<string>
    {
        public int Id { get; set; }
        public string FieldName => "Title";
        public string Value { get; set; }
    }
}