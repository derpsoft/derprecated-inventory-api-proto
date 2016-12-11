namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/user/{Id}/lastName", "PUT,POST,PATCH")]
    [Authenticate]
    public class UpdateUserLastName : IUpdatableField<string>
    {
        public string FieldName => "LastName";
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
