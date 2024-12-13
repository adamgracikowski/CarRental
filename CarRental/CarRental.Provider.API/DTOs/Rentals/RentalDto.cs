namespace CarRental.Provider.API.DTOs.Rentals;

public sealed record RentalDto(
	int Id,
	DateTime GeneratedAt,
	DateTime ExpiresAt
);