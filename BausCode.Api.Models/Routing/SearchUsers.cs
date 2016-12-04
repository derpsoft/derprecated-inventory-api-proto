namespace BausCode.Api.Models.Routing
{
    using Dto;
    using ServiceStack;
    using ServiceStack.Auth;

    [Route("/api/v1/users/search")]
    public class SearchUsers : QueryDb<UserAuth, User>
    {
        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Email",
            ValueFormat = "%{0}%")]
        public string Email { get; set; }

        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }
    }
}
