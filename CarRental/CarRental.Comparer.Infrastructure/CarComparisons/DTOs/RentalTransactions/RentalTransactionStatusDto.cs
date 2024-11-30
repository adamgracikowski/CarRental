namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;

public sealed record RentalTransactionStatusDto(
    int RentalTransactionId,
    string Status
);