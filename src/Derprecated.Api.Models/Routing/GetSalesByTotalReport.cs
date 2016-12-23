namespace Derprecated.Api.Models.Routing
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    [Route("/api/v1/reports/salesByTotal", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanReadReports)]
    public class GetSalesByTotalReport : IReturn<ReportResponse<Dictionary<DateTime, List<int>>>>
    {
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public string GroupBy { get; set; } = DateSegments.Day;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7);
    }
}
