using System.Data;
using BausCode.Api.Models;

namespace BausCode.Api.Handlers
{
    internal class PriceHandler
    {
        public PriceHandler(IDbConnection db, UserSession user)
        {
            Db = db;
            User = user;
        }

        private IDbConnection Db { get; }
        private UserSession User { get; set; }

        public decimal GetPrice(Variant variant)
        {
            /* 
             * If no price override found, return default.
             */
            return variant.Price;
        }
    }
}