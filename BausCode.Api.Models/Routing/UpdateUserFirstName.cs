using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/user/{Id}/firstName", "PUT,POST,PATCH")]
    public class UpdateUserFirstName : IUpdatableField<string>
    {
        public int Id { get; set; }
        public string FieldName => "FirstName";
        public string Value { get; set; }
    }
}