using CarRental.Provider.API.Requests.Customers.DTOs;
using FluentValidation;

namespace CarRental.Provider.API.Requests.Customers.Validators;

public sealed class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .EmailAddress()
            .WithMessage("Invalid {PropertyName}.")
            .MaximumLength(ValidatorsConstants.Customer.EmailAddressMaxLength)
            .WithMessage("{PropertyName} cannot be longer than {MaxLength} characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .MinimumLength(ValidatorsConstants.Customer.FirstNameMinLength)
            .WithMessage("{PropertyName} must be at least {MinLength} characters long.")
            .MaximumLength(ValidatorsConstants.Customer.FirstNameMaxLength)
            .WithMessage("{PropertyName} cannot be longer than {MaxLength} characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .MinimumLength(ValidatorsConstants.Customer.LastNameMinLength)
            .WithMessage("{PropertyName} must be at least {MinLength} characters long.")
            .MaximumLength(ValidatorsConstants.Customer.LastNameMaxLength)
            .WithMessage("{PropertyName} cannot be longer than {MaxLength} characters.");
    }
}