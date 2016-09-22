using ServiceStack;
using ServiceStack.FluentValidation;

namespace BausCode.Api.Models.Validation
{
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