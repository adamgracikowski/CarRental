using CarRental.Provider.API.DTOs.Customers;
using CarRental.Provider.API.Requests.Offers.Commands;
using FluentValidation;

namespace CarRental.Provider.API.Requests.Offers.Validators;

public sealed class ChooseOfferCommandValidator : AbstractValidator<ChooseOfferCommand>
{
    private readonly IValidator<CustomerDto> customerDtoValidator;

    public ChooseOfferCommandValidator(IValidator<CustomerDto> customerDtoValidator)
    {
        this.customerDtoValidator = customerDtoValidator;

        RuleFor(c => c.Id)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(c => c.Audience)
            .NotEmpty()
            .WithMessage("{PropertyName} must be non-empty.");

        RuleFor(c => c.CustomerDto)
            .SetValidator(this.customerDtoValidator)
            .OverridePropertyName(string.Empty);
    }
}