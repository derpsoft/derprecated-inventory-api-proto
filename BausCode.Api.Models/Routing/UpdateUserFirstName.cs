namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/user/{Id}/firstName", "PUT,POST,PATCH")]
    public class UpdateUserFirstName : IUpdatableField<string>
    {
        public string FieldName => "FirstName";
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
