﻿using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using FluentValidation;

namespace CarRental.Comparer.API.Validators.RentalTransactions;

public sealed class AcceptRentalReturnDtoValidator : AbstractValidator<AcceptRentalReturnDto>
{
	public AcceptRentalReturnDtoValidator()
	{
		RuleFor(x => x.Latitude)
			.InclusiveBetween(ValidatorsConstants.AcceptRentalReturnDtoConstants.MinLatitude, ValidatorsConstants.AcceptRentalReturnDtoConstants.MaxLatitude)
			.WithMessage("Latitude must be between {From} and {To}.");

		RuleFor(x => x.Longitude)
			.InclusiveBetween(ValidatorsConstants.AcceptRentalReturnDtoConstants.MinLongitude, ValidatorsConstants.AcceptRentalReturnDtoConstants.MaxLatitude)
			.WithMessage("Longitude must be between {From} and {To}.");

		RuleFor(x => x.Description)
			.MaximumLength(ValidatorsConstants.AcceptRentalReturnDtoConstants.DescriptionMaxLength)
			.When(x => x.Description != null)
			.WithMessage("Description must be null or up to {MaxLength} characters long.");
	}
}