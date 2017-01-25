namespace Derprecated.Api.Models.Validation
{
    using Dto;
    using ServiceStack;
    using ServiceStack.FluentValidation;

    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleSet(ApplyTo.Get, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);
            });

            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(x => x.ProductId)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.Quantity)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.Total)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.VendorId)
                    .GreaterThanOrEqualTo(1);
            });
        }
    }

    public class SalesValidator : AbstractValidator<Sales>
    {
        public SalesValidator()
        {
            RuleSet(ApplyTo.Get, () =>
            {
                RuleFor(x => x.Skip)
                    .GreaterThanOrEqualTo(0);

                RuleFor(x => x.Take)
                    .GreaterThanOrEqualTo(1);
            });
        }
    }
}
