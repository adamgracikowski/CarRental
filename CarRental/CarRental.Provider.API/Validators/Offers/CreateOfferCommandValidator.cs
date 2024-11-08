using CarRental.Provider.API.DTOs.Offers;
using CarRental.Provider.API.Requests.Offers.Commands;
using FluentValidation;

namespace CarRental.Provider.API.Validators.Offers;

public sealed class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
{
    private readonly IValidator<CreateOfferDto> createOfferDtoValidator;

    public CreateOfferCommandValidator(IValidator<CreateOfferDto> createOfferDtoValidator)
    {
        this.createOfferDtoValidator = createOfferDtoValidator;

        RuleFor(c => c.CarId)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(c => c.CreateOfferDto)
            .SetValidator(this.createOfferDtoValidator)
            .OverridePropertyName(string.Empty);
    }
}