namespace Derprecated.Api.Models.Validation
{
    using Dto;
    using ServiceStack;
    using ServiceStack.FluentValidation;

    public class ImageValidator : AbstractValidator<Image>
    {
        public ImageValidator()
        {
            RuleSet(ApplyTo.Get, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);
            });

            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(x => x.Id)
                    .Must(x => x == default(int))
                    .WithMessage("{0} may not be set when creating a Image");

                RuleFor(x => x.RowVersion)
                    .Must(x => x == default(long))
                    .WithMessage("{0} may not be set when creating a Image");
            });

            RuleSet(ApplyTo.Put | ApplyTo.Patch, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.RowVersion)
                    .NotEmpty()
                    .Must(x => x >= 1L);
            });

            RuleSet(ApplyTo.Delete, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.RowVersion)
                    .NotEmpty()
                    .Must(x => x >= 1L);
            });
        }
    }
}
