namespace Derprecated.Api.Models.Validation
{
    using Dto;
    using ServiceStack;
    using ServiceStack.FluentValidation;

    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
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
                    .WithMessage("{0} may not be set when creating a Location");

                RuleFor(x => x.RowVersion)
                    .Must(x => x == default(long))
                    .WithMessage("{0} may not be set when creating a Location");
            });

            RuleSet(ApplyTo.Put | ApplyTo.Patch | ApplyTo.Delete, () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(1);

                RuleFor(x => x.RowVersion)
                    .NotEmpty()
                    .Must(x => x >= 1L);
            });

            RuleSet(ApplyTo.Put | ApplyTo.Post | ApplyTo.Patch, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .Length(0, 50);

                RuleFor(x => x.WarehouseId)
                    .GreaterThanOrEqualTo(1);
            });
        }
    }
}
