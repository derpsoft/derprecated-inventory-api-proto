using ServiceStack;

namespace BausCode.Api.Models.Routing
{
    [Route("/api/v1/products/search", "POST")]
    public class SearchProducts : QueryDb<Product>
    {
        [QueryDbField(Term = QueryTerm.And,  Template = "FREETEXT({Field}, {Value})", Field = "Description",
            ValueFormat = "{0}")]
        public string Query { get; set; }
    }
}