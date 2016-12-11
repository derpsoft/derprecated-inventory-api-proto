namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/products/{Id}/description", "PUT,POST,PATCH")]
    [Authenticate]
    public class UpdateProductDescription : IUpdatableField<string>, IReturn<ProductResponse>
    {
        public string FieldName => "Description";
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
