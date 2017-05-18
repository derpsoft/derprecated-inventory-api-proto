namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Models;
    using ServiceStack.Auth;
    using ServiceStack.OrmLite;

    public class ReportHandler
    {
        private static readonly string[] AcceptableGroupBy = {DateSegments.Day, DateSegments.Week, DateSegments.Month};

        public ReportHandler(IDbConnection db, IAuthSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private IAuthSession User { get; }

        public Dictionary<DateTime, decimal> GetSalesByVendor(DateTime startDate, DateTime endDate, string groupBy,
                                                              int vendorId)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            if (!AcceptableGroupBy.Contains(groupBy))
                throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy,
                    $"must be one of {string.Join(", ", AcceptableGroupBy)}");

            return
                Db.Dictionary<DateTime, decimal>(
                    $@"
                SELECT
                    CAST(MIN([Timestamp]) AS DATE)
                    , SUM([Total])
                FROM
                    [Sale]
                WHERE
                    [Timestamp] BETWEEN @startDate AND @endDate
                    AND [VendorId] = @vendorId
                GROUP BY DATEPART({groupBy}, [Timestamp])
                 ",
                    new
                    {
                        startDate,
                        endDate,
                        vendorId
                    });
        }


        public Dictionary<DateTime, decimal> GetSalesByProduct(DateTime startDate, DateTime endDate, string groupBy,
                                                               int productId)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            if (!AcceptableGroupBy.Contains(groupBy))
                throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy,
                    $"must be one of {string.Join(", ", AcceptableGroupBy)}");

            return
                Db.Dictionary<DateTime, decimal>(
                    $@"
                SELECT
                    CAST(MIN([Timestamp]) AS DATE)
                    , SUM([Total])
                FROM
                    [Sale]
                WHERE
                    [Timestamp] BETWEEN @startDate AND @endDate
                    AND [ProductId] = @productId
                GROUP BY DATEPART({groupBy}, [Timestamp])",
                    new
                    {
                        startDate,
                        endDate,
                        productId
                    });
        }

        public Dictionary<DateTime, int> GetDispatchedInventory(DateTime startDate, DateTime endDate, string groupBy)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            if (!AcceptableGroupBy.Contains(groupBy))
                throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy,
                    $"must be one of {string.Join(", ", AcceptableGroupBy)}");

            return
                Db.Dictionary<DateTime, int>(
                   $@"
                SELECT
                    CAST(MIN([CreateDate]) AS DATE)
                    , SUM([Quantity])
                FROM
                    [InventoryTransaction]
                WHERE
                    [CreateDate] BETWEEN @startDate AND @endDate
                    AND [TransactionType] = @transactionType
                GROUP BY DATEPART({groupBy}, [CreateDate])",
                    new
                    {
                        startDate,
                        endDate,
                        groupBy,
                        transactionType = InventoryTransactionTypes.Out
                    });
        }

        public Dictionary<DateTime, int> GetReceivedInventory(DateTime startDate, DateTime endDate, string groupBy)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            if (!AcceptableGroupBy.Contains(groupBy))
                throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy,
                    $"must be one of {string.Join(", ", AcceptableGroupBy)}");

            return
                Db.Dictionary<DateTime, int>(
                    $@"
                SELECT
                    CAST(MIN([CreateDate]) AS DATE)
                    , SUM([Quantity])
                FROM
                    [InventoryTransaction]
                WHERE
                    [CreateDate] BETWEEN @startDate AND @endDate
                    AND [TransactionType] = @transactionType
                GROUP BY DATEPART({groupBy}, [CreateDate])",
                    new
                    {
                        startDate,
                        endDate,
                        groupBy,
                        transactionType = InventoryTransactionTypes.In
                    });
        }

        public int GetSalesByUser(string userId, DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            return
                Db.Scalar<int>(
                    $@"
                SELECT
                    COUNT([Id])
                FROM
                    [Order]
                WHERE
                    BillByUserAuthId = @userId
                    AND [BillDate] BETWEEN @startDate AND @endDate
                ;",
                    new
                    {
                        userId,
                        startDate,
                        endDate,
                    });
        }

        public decimal GetRevenueByUser(string userId, DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            return
                Db.Scalar<decimal>(
                    $@"
                SELECT
                    SUM([Price])
                FROM
                    [Order]
                WHERE
                    BillByUserAuthId = @userId
                    AND [BillDate] BETWEEN @startDate AND @endDate
                ;",
                    new
                    {
                        userId,
                        startDate,
                        endDate,
                    });
        }

        public int GetListingsByUser(string userId, DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            return 0;
        }

        public int GetShippedInventoryByUser(string userId, DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            return Db.Scalar<int>($@"
                SELECT
                    SUM([Quantity])
                FROM
                    [InventoryTransaction]
                WHERE
                    UserAuthId = @userId
                    AND [CreateDate] BETWEEN @startDate AND @endDate
                    AND [TransactionType] = @transactionType
                ;",
                    new
                    {
                        userId,
                        startDate,
                        endDate,
                        transactiontype = InventoryTransactionTypes.Out,
                    });
        }

        public int GetReceivedInventoryByUser(string userId, DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            return Db.Scalar<int>($@"
                SELECT
                    CAST(SUM([Quantity]) AS INTEGER)
                FROM
                    [InventoryTransaction]
                WHERE
                    UserAuthId = @userId
                    AND [CreateDate] BETWEEN @startDate AND @endDate
                    AND [TransactionType] = @transactionType
                ;",
                    new
                    {
                        userId,
                        startDate,
                        endDate,
                        transactiontype = InventoryTransactionTypes.In,
                    });
        }

        public Dictionary<DateTime, decimal> GetSalesByTotal(DateTime startDate, DateTime endDate, string groupBy)
        {
            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} should come before {nameof(endDate)}");

            if (!AcceptableGroupBy.Contains(groupBy))
                throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy,
                    $"must be one of {string.Join(", ", AcceptableGroupBy)}");

            return
                Db.Dictionary<DateTime, decimal>(
                    $@"
                SELECT
                    CAST(MIN([Timestamp]) AS DATE)
                    , SUM([Total])
                FROM
                    [Sale]
                WHERE
                    [Timestamp] BETWEEN @startDate AND @endDate
                GROUP BY DATEPART({groupBy}, [Timestamp])
                 ",
                    new
                    {
                        startDate,
                        endDate
                    });
        }
    }
}
