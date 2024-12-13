using CarRental.Comparer.API.Requests.RentalTransactions.Commands;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using FluentValidation;

namespace CarRental.Comparer.API.Validators.RentalTransactions;

public sealed class AcceptRentalReturnCommandValidator : AbstractValidator<AcceptRentalReturnCommand>
{
	private readonly IValidator<AcceptRentalReturnDto> acceptRentalReturnDtoValidator;

	public AcceptRentalReturnCommandValidator(IValidator<AcceptRentalReturnDto> acceptRentalReturnDtoValidator)
	{
		this.acceptRentalReturnDtoValidator = acceptRentalReturnDtoValidator;

		RuleFor(x => x.EmployeeEmail)
			.NotEmpty()
			.WithMessage("{PropertyName} is required.")
			.EmailAddress()
			.WithMessage("Invalid {PropertyName}.");

		RuleFor(x => x.Id)
			.GreaterThan(0)
			.WithMessage("{PropertyName} must be greater than {ComparisonProperty}.");

		RuleFor(x => x.AcceptRentalReturn)
			.SetValidator(this.acceptRentalReturnDtoValidator)
			.OverridePropertyName(string.Empty);
	}
}