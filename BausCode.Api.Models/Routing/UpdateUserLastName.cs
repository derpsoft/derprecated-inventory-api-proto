using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/user/{Id}/lastName", "PUT,POST,PATCH")]
    public class UpdateUserLastName : IUpdatableField<string>
    {
        public int Id { get; set; }
        public string FieldName => "LastName";
        public string Value { get; set; }
    }
}