namespace Derprecated.Api.Models.Validation
{
    using Dto;
    using ServiceStack;
    using ServiceStack.FluentValidation;

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleSet(ApplyTo.Get, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);
            });

            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(x => x.Id)
                    .Must(x => x == default(int))
                    .WithMessage("{0} may not be set when creating a Product");

                RuleFor(x => x.RowVersion)
                    .Must(x => x == default(long))
                    .WithMessage("{0} may not be set when creating a Product");

                RuleFor(x => x.Title)
                    .NotEmpty()
                    .Length(0, 50);

                RuleFor(x => x.Sku)
                    .NotEmpty()
                    .Length(0, 200);
            });

            RuleSet(ApplyTo.Put | ApplyTo.Patch, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.RowVersion)
                    .NotEmpty()
                    .Must(x => x >= 1L);

                RuleFor(x => x.Title)
                    .NotEmpty()
                    .Length(0, 50);
            });

            RuleSet(ApplyTo.Delete, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.RowVersion)
                    .NotEmpty()
                    .Must(x => x >= 1L);
            });
        }
    }

    public class ProductImportValidator : AbstractValidator<ProductImport>
    {
        public ProductImportValidator()
        {
            RuleSet(ApplyTo.Post, () =>
            {
                RuleForEach(x => x.Products)
                    .Must(x => x.Id == default(int)).WithMessage("{0} may not be set when creating a Product")
                    .Must(x => x.RowVersion == default(long)).WithMessage("{0} may not be set when creating a Product");
            });
        }
    }

    public class ProductBySkuValidator : AbstractValidator<ProductBySku>
    {
        public ProductBySkuValidator()
        {
            RuleSet(ApplyTo.Get, () =>
            {
                RuleFor(x => x.Sku)
                    .NotEmpty();
            });
        }
    }
}
