namespace Derprecated.Api.Models.Validation
{
    using Routing;
    using ServiceStack.FluentValidation;

    public class CreateVendorValidator : AbstractValidator<CreateVendor>
    {
        public CreateVendorValidator()
        {
            RuleFor(x => x.Vendor)
                .NotNull();

            RuleFor(x => x.Vendor.Name)
                .NotEmpty()
                .Length(0, 50);
        }
    }
}
