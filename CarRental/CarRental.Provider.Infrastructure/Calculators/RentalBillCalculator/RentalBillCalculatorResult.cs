namespace CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;

public sealed record RentalBillCanculatorResult(
    int NumberOfDays,
    decimal RentalTotalPrice,
    decimal InsuranceTotalPrice,
    decimal SummaryTotalPrice
);