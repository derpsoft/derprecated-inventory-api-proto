namespace Derprecated.Api.Models.Validation
{
    using Dto;
    using ServiceStack;
    using ServiceStack.FluentValidation;

    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleSet(ApplyTo.Get | ApplyTo.Patch | ApplyTo.Put | ApplyTo.Delete, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);
            });

            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(x => x.Id)
                    .Must(x => x == default(int))
                    .WithMessage("{0} may not be set when creating a Category");

                RuleFor(x => x.RowVersion)
                    .Must(x => x == default(long))
                    .WithMessage("{0} may not be set when creating a Category");
            });

            RuleSet(ApplyTo.Put | ApplyTo.Patch | ApplyTo.Delete, () =>
            {
                RuleFor(x => x.RowVersion)
                    .NotEmpty()
                    .Must(x => x >= 1L);
            });

            RuleSet(ApplyTo.Post | ApplyTo.Put | ApplyTo.Patch, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .Length(0, 50);
            });
        }
    }
}
