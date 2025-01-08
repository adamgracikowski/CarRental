namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Rentals;

public sealed record ExternalRentalStatusDto
(
	int RentalId,
	string CarState
);
