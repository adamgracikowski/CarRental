using CarRental.Provider.API.DTOs.RentalReturns;
using FluentValidation;

namespace CarRental.Provider.API.Validators.RentalReturns;

public sealed class AcceptRentalReturnDtoValidator : AbstractValidator<AcceptRentalReturnDto>
{
    public AcceptRentalReturnDtoValidator()
    {
        RuleFor(a => a.Description)
            .MaximumLength(ValidatorsConstants.AcceptRentalReturnConstants.DescriptionMaxLength)
            .WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");

        When(a => a.Image is not null, () =>
        {
            RuleFor(a => a.Image)
                .Must(BeValidFileSize!)
                .WithMessage("The {PropertyName} size cannot exceed 5 MB.")
                .Must(BeValidImageType!)
                .WithMessage($"Allowed file types are: {string.Join(", ", ValidatorsConstants.AcceptRentalReturnConstants.ImageAllowedExtensions)}.");
        });

        RuleFor(a => a.Latitude)
            .NotNull()
			.WithMessage("{PropertyName} is required.")
			.InclusiveBetween(ValidatorsConstants.LocalizationConstants.LatitudeMin, ValidatorsConstants.LocalizationConstants.LatitudeMax)
            .WithMessage("{PropertyName} must be between {From} and {To}.");

        RuleFor(a => a.Longitude)
			.NotNull()
			.WithMessage("{PropertyName} is required.")
			.InclusiveBetween(ValidatorsConstants.LocalizationConstants.LongitudeMin, ValidatorsConstants.LocalizationConstants.LongitudeMax)
            .WithMessage("{PropertyName} must be between {From} and {To}.");
    }

    private bool BeValidFileSize(IFormFile file)
    {
        return file.Length <= ValidatorsConstants.AcceptRentalReturnConstants.ImageMaxSize;
    }

    private bool BeValidImageType(IFormFile file)
    {
        return ValidatorsConstants.AcceptRentalReturnConstants.ImageAllowedMimeTypes.Contains(file.ContentType.ToLower());
    }
}