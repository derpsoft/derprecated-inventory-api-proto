namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/{Id}/title", "PUT,POST,PATCH")]
    [Authenticate]
    public class UpdateProductTitle : IUpdatableField<string>, IReturn<ProductResponse>
    {
        public string FieldName => "Title";
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
