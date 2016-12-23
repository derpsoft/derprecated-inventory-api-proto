namespace Derprecated.Api.Models.Validation
{
    using System;
    using System.Linq;
    using Routing;
    using ServiceStack.FluentValidation;

    public class GetSalesByProductReportValidator : AbstractValidator<GetSalesByProductReport>
    {
        public GetSalesByProductReportValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .ExclusiveBetween(DateTime.MinValue, DateTime.MaxValue)
                .GreaterThan(x => x.StartDate);

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .ExclusiveBetween(DateTime.MinValue, DateTime.MaxValue)
                .LessThan(x => x.EndDate);

            var acceptableGroupBy = new[] {DateSegments.Day, DateSegments.Week, DateSegments.Month};
            RuleFor(x => x.GroupBy)
                .NotEmpty()
                .Must(v => acceptableGroupBy.Contains(v));
        }
    }
}
