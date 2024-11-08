using CarRental.Provider.API.DTOs.Offers;
using FluentValidation;

namespace CarRental.Provider.API.Validators.Offers;

public class CreateOfferDtoValidator : AbstractValidator<CreateOfferDto>
{
    public CreateOfferDtoValidator()
    {
        RuleFor(c => c.Age)
            .GreaterThanOrEqualTo(ValidatorsConstants.CreateOfferConstants.AgeMin)
            .WithMessage("{PropertyName} must be at least {ComparisonValue}.");

        RuleFor(c => c.DrivingLicenseYears)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(c => c.Latitude)
            .InclusiveBetween(ValidatorsConstants.LocalizationConstants.LatitudeMin, ValidatorsConstants.LocalizationConstants.LatitudeMax)
            .WithMessage("{PropertyName} must be between {From} and {To}.");

        RuleFor(c => c.Longitude)
            .InclusiveBetween(ValidatorsConstants.LocalizationConstants.LongitudeMin, ValidatorsConstants.LocalizationConstants.LongitudeMax)
            .WithMessage("{PropertyName} must be between {From} and {To}.");
    }
}