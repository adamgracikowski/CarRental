namespace CarRental.Provider.Infrastructure.Calculators.OfferCalculator;

public sealed record OfferCalculatorInput(
    int DrivingLicenseYears,
    int Age,
    decimal Latitude,
    decimal Longitude,
    decimal BaseRentalPricePerDay,
    decimal BaseInsurancePricePerDay
);