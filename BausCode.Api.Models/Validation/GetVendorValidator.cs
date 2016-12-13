namespace BausCode.Api.Models.Validation
{
    using Routing;
    using ServiceStack.FluentValidation;

    public class GetVendorValidator : AbstractValidator<GetVendor>
    {
        public GetVendorValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1);
        }
    }
}
