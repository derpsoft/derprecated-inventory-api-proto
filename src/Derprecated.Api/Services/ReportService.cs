namespace Derprecated.Api.Services
{
    using System;
    using System.Collections.Generic;
    using Handlers;
    using Models;
    using Models.Routing;
    using ServiceStack;

    public class ReportService : BaseService
    {
        public object Any(GetSalesByTotalReport request)
        {
            var result = new ReportResponse<Dictionary<DateTime, int>>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Report = handler.GetSalesByTotal(request.StartDate, request.EndDate, request.GroupBy);

            return result;
        }

        public object Any(GetSalesByProductReport request)
        {
            var result = new ReportResponse<Dictionary<DateTime, int>>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Report = handler.GetSalesByProduct(request.StartDate, request.EndDate, request.GroupBy,
                request.ProductId);

            return result;
        }

        public object Any(GetSalesByVendorReport request)
        {
            var result = new ReportResponse<Dictionary<DateTime, int>>();
            var handler = new ReportHandler(Db, CurrentSession);

            result.Report = handler.GetSalesByVendor(request.StartDate, request.EndDate, request.GroupBy,
                request.VendorId);

            return result;
        }

        public object Any(LogSale request)
        {
            var result = new SaleResponse();
            var handler = new SaleHandler(Db, CurrentSession);
            var sale = new Sale().PopulateWith(request);

            handler.Create(sale);
            result.Sale = Models.Dto.Sale.From(sale);

            return result;
        }
    }
}
