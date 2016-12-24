namespace Derprecated.Api.Models.Validation
{
    using Routing;
    using ServiceStack.FluentValidation;

    public class UserTypeaheadValidator : AbstractValidator<UserTypeahead>
    {
        public UserTypeaheadValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty()
                .Length(1, 20);
        }
    }
}
