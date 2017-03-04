namespace Derprecated.Api.Models.Validation
{
    using Dto;
    using ServiceStack;
    using ServiceStack.FluentValidation;

    public sealed class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleSet(ApplyTo.Get, () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            });

            RuleSet(ApplyTo.Put | ApplyTo.Patch, () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            });

            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(x => x.Id)
                    .Must(x => x == default(string))
                    .WithMessage("{0} may not be set when creating a User");
            });

            RuleSet(ApplyTo.Delete, () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            });
        }
    }
}
