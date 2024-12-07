using CarRental.Common.Core.Enums;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using FluentValidation;
namespace CarRental.Comparer.API.Validators.RentalTransactions;

public sealed class GetRentalTransactionsForEmployeeQueryValidator : AbstractValidator<GetRentalTransactionsForEmployeeQuery>
{
	public GetRentalTransactionsForEmployeeQueryValidator()
	{
		RuleFor(x => x.EmployeeEmail)
			.NotEmpty()
			.WithMessage("{PropertyName} is required.")
			.EmailAddress()
			.WithMessage("Invalid {PropertyName}.");

		RuleFor(x => x.Page)
			.NotEmpty()
			.GreaterThan(0)
			.WithMessage("{PropertyName} must be greater than {ComparisonProperty}.");

		RuleFor(x => x.Size)
			.NotEmpty()
			.GreaterThan(0)
			.WithMessage("{PropertyName} must be greater than {ComparisonProperty}.");

		RuleFor(x => x.Status)
			.NotEmpty()
			.IsEnumName(typeof(RentalStatus), caseSensitive: false)
			.WithMessage($"Status must be one of the following: {string.Join(", ", Enum.GetNames(typeof(RentalStatus)))}.");
	}
}