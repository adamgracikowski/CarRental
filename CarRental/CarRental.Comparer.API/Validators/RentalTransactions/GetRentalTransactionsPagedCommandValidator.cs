using CarRental.Common.Core.Enums;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using FluentValidation;
namespace CarRental.Comparer.API.Validators.RentalTransactions;

public sealed class GetRentalTransactionsPagedCommandValidator : AbstractValidator<GetRentalTransactionsPagedQuery>
{
	public GetRentalTransactionsPagedCommandValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.WithMessage("{PropertyName} is required.")
			.EmailAddress()
			.WithMessage("Invalid {PropertyName}.");

		RuleFor(x => x.PageNumber)
			.NotEmpty()
			.GreaterThan(0)
			.WithMessage("{PropertyName} must be greater than 0.");

		RuleFor(x => x.PageSize)
			.NotEmpty()
			.GreaterThan(0)
			.WithMessage("{PropertyName} must be greater than 0.");

		RuleFor(x => x.Status)
			.NotEmpty()
			.IsEnumName(typeof(RentalStatus), caseSensitive: false)
			.WithMessage($"Status must be one of the following: {string.Join(", ", Enum.GetNames(typeof(RentalStatus)))}.");
	}
}