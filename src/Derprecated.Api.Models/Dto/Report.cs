namespace Derprecated.Api.Models.Dto
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    public sealed class Report<T>
    {
        public T Result { get; set; }
    }

    [Route("/api/v1/reports/dashboard", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanReadReports)]
    public sealed class DashboardReport : IReturn<Dto<DashboardReport>>
    {
        public Dictionary<DateTime, int> Dispatched { get; set; }

        public DateTime EndDate { get; set; } =
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddTicks(-1);

        public string GroupBy { get; set; } = DateSegments.Day;

        public Dictionary<DateTime, int> Received { get; set; }

        public Dictionary<DateTime, decimal> Sales { get; set; }

        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
}
