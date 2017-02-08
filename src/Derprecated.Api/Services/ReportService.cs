namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using Handlers;
    using Models;
    using Models.Dto;
    using Models.Routing;
    using ServiceStack;

    public class ReportService : BaseService
    {
        public object Any(GetSalesByTotalReport request)
        {
            var result = new ReportResponse<Dictionary<DateTime, decimal>>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Report = handler.GetSalesByTotal(request.StartDate, request.EndDate, request.GroupBy);

            return result;
        }

        public object Any(GetSalesByProductReport request)
        {
            var result = new ReportResponse<Dictionary<DateTime, decimal>>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Report = handler.GetSalesByProduct(request.StartDate, request.EndDate, request.GroupBy,
                request.ProductId);

            return result;
        }

        public object Any(GetSalesByVendorReport request)
        {
            var result = new ReportResponse<Dictionary<DateTime, decimal>>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Report = handler.GetSalesByVendor(request.StartDate, request.EndDate, request.GroupBy,
                request.VendorId);

            return result;
        }

        public object Any(DashboardReport request)
        {
            var result = new Dto<DashboardReport>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Result = request;
            result.Result.Dispatched = Math.Abs(handler.GetDispatchedTotal(request.StartDate, request.EndDate));
            result.Result.Received = Math.Abs(handler.GetReceivedTotal(request.StartDate, request.EndDate));
            result.Result.TotalSales = handler.GetSalesByTotal(request.StartDate, request.EndDate);

            return result;
        }
    }
}
