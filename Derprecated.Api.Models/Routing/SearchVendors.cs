namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/vendors/search")]
    [Authenticate]
    public class SearchVendors: QueryDb<Vendor, Dto.Vendor>
    {
        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Name",
            ValueFormat = "%{0}%")]
        public string Name { get; set; }

        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }
    }
}
