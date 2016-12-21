namespace Derprecated.Api.Models.Validation
{
    using Routing;
    using ServiceStack.FluentValidation;

    public class GetUserValidator : AbstractValidator<GetUser>
    {
        public GetUserValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1);
        }
    }
}
