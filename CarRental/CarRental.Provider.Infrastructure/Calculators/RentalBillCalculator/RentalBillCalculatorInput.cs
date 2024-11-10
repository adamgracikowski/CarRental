namespace CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;

public sealed record RentalBillCalculatorInput(
    DateTime RentedAt,
    DateTime ReturnedAt,
    decimal RentalPricePerDay,
    decimal InsurancePricePerDay
);