namespace Derprecated.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/locations/search")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanManageLocations, Permissions.CanReadLocations)]
    public class SearchLocations : QueryDb<Location, Dto.Location>
    {
        [QueryDbField(Term = QueryTerm.Or)]
        public int Id { get; set; }

        [QueryDbField(Term = QueryTerm.Or, Template = "LOWER({Field}) like {Value}", Field = "Row",
            ValueFormat = "%{0}%")]
        public string Row { get; set; }

        [QueryDbField(Term = QueryTerm.Or)]
        public int WarehouseId { get; set; }
    }
}
