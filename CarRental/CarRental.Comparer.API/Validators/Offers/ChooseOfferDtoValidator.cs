using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using FluentValidation;

namespace CarRental.Comparer.API.Validators.Offers;

public sealed class ChooseOfferDtoValidator : AbstractValidator<ChooseOfferDto>
{
    public ChooseOfferDtoValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .EmailAddress()
            .WithMessage("Invalid {PropertyName}.")
            .MaximumLength(ValidatorsConstants.ChooseOfferDtoConstants.EmailAddressMaxLength)
            .WithMessage("{PropertyName} cannot be longer than {MaxLength} characters.");

        RuleFor(x => x.RentalPricePerDay)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.InsurancePricePerDay)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("{PropertyName} must be greater than or equal to 0.");

        RuleFor(x => x.RentedAt)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.CarOuterId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Make)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.YearOfProduction)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.ExpiresAt)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}
