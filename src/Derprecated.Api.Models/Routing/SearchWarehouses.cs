namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/warehouses/search")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageWarehouses, Permissions.CanReadWarehouses)]
    public class SearchWarehouses : QueryDb<Warehouse, Dto.Warehouse>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }

        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Name",
            ValueFormat = "%{0}%")]
        public string Name { get; set; }
    }
}
