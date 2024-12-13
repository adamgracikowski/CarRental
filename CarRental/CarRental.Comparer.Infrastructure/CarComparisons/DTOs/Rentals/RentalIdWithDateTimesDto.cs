namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;

public sealed record RentalIdWithDateTimesDto(
    string Id, 
    DateTime GeneratedAt,
    DateTime ExpiresAt
);