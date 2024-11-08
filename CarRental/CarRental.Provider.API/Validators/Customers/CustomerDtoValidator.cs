using CarRental.Provider.API.DTOs.Customers;
using FluentValidation;

namespace CarRental.Provider.API.Validators.Customers;

public sealed class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .EmailAddress()
            .WithMessage("Invalid {PropertyName}.")
            .MaximumLength(ValidatorsConstants.CustomerConstants.EmailAddressMaxLength)
            .WithMessage("{PropertyName} cannot be longer than {MaxLength} characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .MinimumLength(ValidatorsConstants.CustomerConstants.FirstNameMinLength)
            .WithMessage("{PropertyName} must be at least {MinLength} characters long.")
            .MaximumLength(ValidatorsConstants.CustomerConstants.FirstNameMaxLength)
            .WithMessage("{PropertyName} cannot be longer than {MaxLength} characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .MinimumLength(ValidatorsConstants.CustomerConstants.LastNameMinLength)
            .WithMessage("{PropertyName} must be at least {MinLength} characters long.")
            .MaximumLength(ValidatorsConstants.CustomerConstants.LastNameMaxLength)
            .WithMessage("{PropertyName} cannot be longer than {MaxLength} characters.");
    }
}