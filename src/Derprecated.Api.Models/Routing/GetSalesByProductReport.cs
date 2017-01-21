namespace Derprecated.Api.Models.Routing
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    [Route("/api/v1/reports/salesByProduct", "GET")]
    [Authenticate]
    [RequiresAnyPermission(Permissions.CanDoEverything, Permissions.CanReadReports)]
    public class GetSalesByProductReport : IReturn<ReportResponse<Dictionary<DateTime, int>>>
    {
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public string GroupBy { get; set; } = DateSegments.Day;

        [Required]
        public int ProductId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7);
    }
}
