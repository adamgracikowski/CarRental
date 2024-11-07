using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using Microsoft.Extensions.Options;

namespace CarRental.Provider.Infrastructure.Calculators.OfferCalculator;

public sealed class OfferCalculatorService : IOfferCalculatorService
{
    private readonly OfferCalculatorOptions options;
    private readonly IDateTimeProvider dateTimeProvider;

    public OfferCalculatorService(
        IOptions<OfferCalculatorOptions> options,
        IDateTimeProvider dateTimeProvider)
    {
        this.options = options.Value;
        this.dateTimeProvider = dateTimeProvider;
    }

    public OfferCalculatorResult CalculatePricePerDay(OfferCalculatorInput input)
    {
        var rentalPricePerDay = input.BaseRentalPricePerDay;
        var insurancePricePerDay = input.BaseInsurancePricePerDay;

        if(input.Age < options.YoungDriverAgeLimit)
        {
            rentalPricePerDay += options.YoungDriverSurcharge * input.BaseRentalPricePerDay;
            insurancePricePerDay += options.YoungDriverSurcharge * input.BaseInsurancePricePerDay;
        }

        if(input.DrivingLicenseYears < options.InexperiencedDriverYearsLimit)
        {
            rentalPricePerDay += options.InexperiencedDriverSurcharge * input.BaseRentalPricePerDay;
            insurancePricePerDay += options.InexperiencedDriverSurcharge * input.BaseInsurancePricePerDay;
        }

        var generatedAt = this.dateTimeProvider.UtcNow;
        var expiresAt = generatedAt.AddMinutes(options.OfferExpirationInMinutes);

        return new OfferCalculatorResult(
            rentalPricePerDay, 
            insurancePricePerDay,
            generatedAt,
            expiresAt
        );
    }
}