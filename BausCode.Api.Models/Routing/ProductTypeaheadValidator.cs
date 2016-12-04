namespace BausCode.Api.Models.Routing
{
    using ServiceStack.FluentValidation;

    public class ProductTypeaheadValidator : AbstractValidator<ProductTypeahead>
    {
        public ProductTypeaheadValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty();
        }
    }
}
