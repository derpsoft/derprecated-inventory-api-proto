namespace Derprecated.Api.Models.Validation
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
        }
    }
}
