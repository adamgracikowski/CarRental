namespace CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;

public sealed record RentalBillCanculatorResult(
    decimal RentalTotalPrice,
    decimal InsuranceTotalPrice,
    decimal SummaryTotalPrice
);