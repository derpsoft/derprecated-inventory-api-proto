namespace BausCode.Api.Models.Validation
{
    using Routing;
    using ServiceStack.FluentValidation;

    public class CreateVendorValidator : AbstractValidator<CreateVendor>
    {
        public CreateVendorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(0, 50);
        }
    }
}
