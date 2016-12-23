namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using ServiceStack.OrmLite;

    public class ReportHandler
    {
        public ReportHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; }

        public Dictionary<DateTime, int> GetSalesByVendor(DateTime startDate, DateTime endDate, string groupBy,
                                                          int vendorId)
        {
            return
                Db.Dictionary<DateTime, int>(
                    $@"
                SELECT 
                    CAST(MIN([Timestamp]) AS DATE)
                    , SUM([Quantity])
                FROM 
                    [Sale]
                WHERE
                    [Timestamp] BETWEEN @startDate AND @endDate
                    AND [VendorId] = @vendorId
                GROUP BY DATEPART({
                        groupBy}, [Timestamp])
                 ",
                    new
                    {
                        startDate,
                        endDate,
                        vendorId
                    });
        }


        public Dictionary<DateTime, int> GetSalesByProduct(DateTime startDate, DateTime endDate, string groupBy,
                                                           int productId)
        {
            return
                Db.Dictionary<DateTime, int>(
                    $@"
                SELECT 
                    CAST(MIN([Timestamp]) AS DATE)
                    , SUM([Quantity])
                FROM 
                    [Sale]
                WHERE
                    [Timestamp] BETWEEN @startDate AND @endDate
                    AND [ProductId] = @productId
                GROUP BY DATEPART({
                        groupBy}, [Timestamp])
                 ",
                    new
                    {
                        startDate,
                        endDate,
                        productId
                    });
        }

        public Dictionary<DateTime, int> GetSalesByTotal(DateTime startDate, DateTime endDate, string groupBy)
        {
            return
                Db.Dictionary<DateTime, int>(
                    $@"
                SELECT 
                    CAST(MIN([Timestamp]) AS DATE)
                    , SUM([Quantity])
                FROM 
                    [Sale]
                WHERE
                    [Timestamp] BETWEEN @startDate AND @endDate
                GROUP BY DATEPART({
                        groupBy}, [Timestamp])
                 ",
                    new
                    {
                        startDate,
                        endDate
                    });
        }
    }
}
