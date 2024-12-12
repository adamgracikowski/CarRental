using CarRental.Comparer.API.Requests.RentalTransactions.Commands;
using FluentValidation;

namespace CarRental.Comparer.API.Validators.RentalTransactions;

public sealed class ReturnRentalTransactionCommandValidator : AbstractValidator<ReturnRentalTransactionCommand>
{
	public ReturnRentalTransactionCommandValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.WithMessage("{PropertyName} is required.")
			.EmailAddress()
			.WithMessage("Invalid {PropertyName}.");

		RuleFor(x => x.Id)
			.GreaterThan(0)
			.WithMessage("{PropertyName} must be greater than {ComparisonProperty}.");
	}
}