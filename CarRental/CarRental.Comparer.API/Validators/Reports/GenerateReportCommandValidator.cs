using CarRental.Comparer.API.DTOs.Reports;
using CarRental.Comparer.API.Requests.RentalTransactions.Commands;
using FluentValidation;

namespace CarRental.Comparer.API.Validators.Reports;

public sealed class GenerateReportCommandValidator : AbstractValidator<GenerateReportCommand>
{
	private readonly IValidator<GenerateReportDto> generateReportDtoValidator;

	public GenerateReportCommandValidator(IValidator<GenerateReportDto> generateReportDtoValidator)
	{
		this.generateReportDtoValidator = generateReportDtoValidator;

		RuleFor(g => g.Email)
			.NotEmpty()
			.WithMessage("{PropertyName} is required.");

		RuleFor(g => g.GenerateReportDto)
			.SetValidator(this.generateReportDtoValidator)
			.OverridePropertyName(string.Empty);
	}
}