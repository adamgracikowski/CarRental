using CarRental.Comparer.API.DTOs.Reports;
using CarRental.Comparer.Infrastructure.Reports;
using FluentValidation;

namespace CarRental.Comparer.API.Validators.Reports;

public sealed class GenerateReportDtoValidator : AbstractValidator<GenerateReportDto>
{
	public GenerateReportDtoValidator()
	{
		RuleFor(g => g.Format)
			.NotEmpty()
			.WithMessage("{PropertyName} is required.")
			.IsEnumName(typeof(ReportFormat), caseSensitive: false)
			.WithMessage("{PropertyValue} is not a valid format.");

		RuleFor(g => g.DateFrom)
			.NotEmpty()
			.WithMessage("{PropertyName} is required.")
			.LessThanOrEqualTo(g => g.DateTo)
			.WithMessage("{PropertyName} must be earlier than or equal to the {ComparisonProperty}.");

		RuleFor(g => g.DateTo)
			.NotEmpty()
			.WithMessage("{PropertyName} is required.")
			.GreaterThanOrEqualTo(g => g.DateFrom)
			.WithMessage("{PropertyName} must be later than or equal to the {ComparisonProperty}.");
	}
}