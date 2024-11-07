using CarRental.Provider.API.Requests.Offers.DTOs;
using FluentValidation;

namespace CarRental.Provider.API.Requests.Offers.Validators;

public class CreateOfferDtoValidator : AbstractValidator<CreateOfferDto>
{
    public CreateOfferDtoValidator()
    {
        RuleFor(c => c.Age)
            .GreaterThanOrEqualTo(ValidatorsConstants.CreateOffer.AgeMin)
            .WithMessage("{PropertyName} must be at least {ComparisonValue}.");

        RuleFor(c => c.DrivingLicenseYears)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(c => c.Latitude)
            .InclusiveBetween(ValidatorsConstants.CreateOffer.LatitudeMin, ValidatorsConstants.CreateOffer.LatitudeMax)
            .WithMessage("{PropertyName} must be between {From} and {To}.");

        RuleFor(c => c.Longitude)
            .InclusiveBetween(ValidatorsConstants.CreateOffer.LongitudeMin, ValidatorsConstants.CreateOffer.LongitudeMax)
            .WithMessage("{PropertyName} must be between {From} and {To}.");
    }
}