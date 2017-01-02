namespace Derprecated.Api.Models.Validation
{
    using Routing;
    using ServiceStack.FluentValidation;

    public class LogSaleValidator : AbstractValidator<LogSale>
    {
        public LogSaleValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.InventoryTransactionId)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Total)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.VendorId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
