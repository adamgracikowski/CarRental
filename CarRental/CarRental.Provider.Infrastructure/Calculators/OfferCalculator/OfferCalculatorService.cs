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

        if(input.Age < this.options.YoungDriverAgeLimit)
        {
            rentalPricePerDay += this.options.YoungDriverSurcharge * input.BaseRentalPricePerDay;
            insurancePricePerDay += this.options.YoungDriverSurcharge * input.BaseInsurancePricePerDay;
        }

        if(input.DrivingLicenseYears < this.options.InexperiencedDriverYearsLimit)
        {
            rentalPricePerDay += this.options.InexperiencedDriverSurcharge * input.BaseRentalPricePerDay;
            insurancePricePerDay += this.options.InexperiencedDriverSurcharge * input.BaseInsurancePricePerDay;
        }

        var generatedAt = this.dateTimeProvider.UtcNow;
        var expiresAt = generatedAt.AddMinutes(this.options.OfferExpirationInMinutes);

        return new OfferCalculatorResult(
            rentalPricePerDay, 
            insurancePricePerDay,
            generatedAt,
            expiresAt
        );
    }
}