namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public object Any(SalesByUser request)
        {
            var dto = new Dto<decimal>();
            var handler = new ReportHandler(Db, CurrentSession);
            dto.Result = handler.GetSalesByUser(CurrentSession.UserAuthId, request.StartDate, request.EndDate);
            return dto;
        }

        public object Any(RevenueByUser request)
        {
            var dto = new Dto<decimal>();
            var handler = new ReportHandler(Db, CurrentSession);
            dto.Result = handler.GetRevenueByUser(CurrentSession.UserAuthId, request.StartDate, request.EndDate);
            return dto;
        }

        public object Any(ListingsByUser request)
        {
            var dto = new Dto<int>();
            var handler = new ReportHandler(Db, CurrentSession);
            dto.Result = handler.GetListingsByUser(CurrentSession.UserAuthId, request.StartDate, request.EndDate);
            return dto;
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
            result.Result.Dispatched = handler.GetDispatchedInventory(request.StartDate, request.EndDate, request.GroupBy);
            result.Result.Received = handler.GetReceivedInventory(request.StartDate, request.EndDate, request.GroupBy);
            result.Result.Sales = handler.GetSalesByTotal(request.StartDate, request.EndDate, request.GroupBy);

            return result;
        }

        public object Any(ShippedByUser request)
        {
            var result = new Dto<int>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Result = handler.GetShippedInventoryByUser(CurrentSession.UserAuthId, request.StartDate, request.EndDate);

            return result;
        }

        public object Any(ReceivedByUser request)
        {
            var result = new Dto<int>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Result = handler.GetReceivedInventoryByUser(CurrentSession.UserAuthId, request.StartDate, request.EndDate);

            return result;
        }
    }
}
