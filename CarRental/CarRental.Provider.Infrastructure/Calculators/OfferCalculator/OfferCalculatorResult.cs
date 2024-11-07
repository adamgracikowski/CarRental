namespace CarRental.Provider.Infrastructure.Calculators.OfferCalculator;

public sealed record OfferCalculatorResult(
    decimal RentalPricePerDay,
    decimal InsurancePricePerDay,
    DateTime GeneratedAt,
    DateTime ExpiresAt
);