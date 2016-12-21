namespace Derprecated.Api.Models.Validation
{
    using Routing;
    using ServiceStack.FluentValidation;

    public class UpdateUserValidator : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1);
        }
    }
}
