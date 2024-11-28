using CarRental.Provider.API.DTOs.RentalReturns;
using CarRental.Provider.API.Requests.Rentals.Commands;
using FluentValidation;

namespace CarRental.Provider.API.Validators.RentalReturns;

public class AcceptRentalReturnCommandValidator : AbstractValidator<AcceptRentalReturnCommand>
{
    private readonly IValidator<AcceptRentalReturnDto> acceptRentalReturnDtoValidator;

    public AcceptRentalReturnCommandValidator(IValidator<AcceptRentalReturnDto> acceptRentalReturnDtoValidator)
    {
        this.acceptRentalReturnDtoValidator = acceptRentalReturnDtoValidator;

        RuleFor(a => a.Id)
            .NotEmpty()
			.WithMessage("{PropertyName} is required.")
			.GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(a => a.Audience)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(a => a.AcceptRentalReturnDto)
            .SetValidator(this.acceptRentalReturnDtoValidator)
            .OverridePropertyName(string.Empty);
    }
}