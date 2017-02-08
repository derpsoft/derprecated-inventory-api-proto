namespace Derprecated.Api.Models.Dto
{
    using System;
    using ServiceStack;

    public sealed class Report : IReturn<Dto<Report>>
    {
    }

    [Route("/api/v1/reports/dashboard", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanReadReports)]
    public sealed class DashboardReport : IReturn<Dto<DashboardReport>>
    {
        public int Dispatched { get; set; }
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public string GroupBy { get; set; } = DateSegments.Day;
        public int Received { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7);
        public decimal TotalSales { get; set; }
    }
}
