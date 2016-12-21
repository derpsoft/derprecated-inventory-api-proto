namespace Derprecated.Api.Models.Validation
{
    using ServiceStack;
    using ServiceStack.FluentValidation;

    public class RegisterValidator : AbstractValidator<Register>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                ;

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6, 32)
                ;
        }
    }
}
