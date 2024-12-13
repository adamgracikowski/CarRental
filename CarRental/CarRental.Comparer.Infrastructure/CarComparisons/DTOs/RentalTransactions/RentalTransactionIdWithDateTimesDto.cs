namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;

public sealed record class RentalTransactionIdWithDateTimesDto(
    int Id, 
    DateTime RentedAt,
    DateTime ExpiresAt
);
