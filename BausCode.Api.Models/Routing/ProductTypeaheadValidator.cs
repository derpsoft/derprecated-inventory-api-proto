using ServiceStack.FluentValidation;

namespace BausCode.Api.Models.Routing
{
    public class ProductTypeaheadValidator : AbstractValidator<ProductTypeahead>
    {
        public ProductTypeaheadValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty();
        }
    }
}