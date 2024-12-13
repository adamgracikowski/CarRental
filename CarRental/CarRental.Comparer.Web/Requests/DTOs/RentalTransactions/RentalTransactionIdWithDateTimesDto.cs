namespace CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

public sealed record RentalTransactionIdWithDateTimesDto(
    int Id, 
    DateTime RentedAt,
    DateTime ExpiresAt
);