namespace Derprecated.Api.Models.Validation
{
    using Dto;
    using ServiceStack;
    using ServiceStack.FluentValidation;

    public sealed class VendorValidator : AbstractValidator<Vendor>
    {
        public VendorValidator()
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
                    .WithMessage("{0} may not be set when creating a Vendor");

                RuleFor(x => x.RowVersion)
                    .Must(x => x == default(long))
                    .WithMessage("{0} may not be set when creating a Vendor");
            });

            RuleSet(ApplyTo.Put | ApplyTo.Patch | ApplyTo.Delete, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.RowVersion)
                    .NotEmpty()
                    .Must(x => x >= 1L);
            });

            RuleSet(ApplyTo.Put | ApplyTo.Post | ApplyTo.Patch, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .Length(0, 50);

                RuleFor(x => x.ContactAddress)
                    .NotEmpty()
                    .Length(0, 400);

                RuleFor(x => x.ContactEmail)
                    .NotEmpty()
                    .Length(0, 256);

                RuleFor(x => x.ContactName)
                    .NotEmpty()
                    .Length(0, 50);

                RuleFor(x => x.ContactPhone)
                    .NotEmpty()
                    .Length(0, 20);
            });
        }
    }

    public sealed class VendorSearchValidator : AbstractValidator<VendorSearch>
    {
    }

    public sealed class VendorCountValidator : AbstractValidator<VendorCount>
    {
    }

    public sealed class VendorsValidator : AbstractValidator<Vendors>
    {
        public VendorsValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Take)
                .GreaterThanOrEqualTo(1);
        }
    }

    public sealed class VendorTypeaheadValidator : AbstractValidator<VendorTypeahead>
    {
        public VendorTypeaheadValidator()
        {
            RuleFor(x => x.Query)
                .Length(1, 20);
        }
    }
}
