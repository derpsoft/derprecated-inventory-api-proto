using BausCode.Api.Models.Dto;
using ServiceStack;
using ServiceStack.Auth;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/users/search")]
    public class SearchUsers : QueryDb<UserAuth, User>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }

        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Email",
            ValueFormat = "%{0}%")]
        public string Email { get; set; }
    }
}