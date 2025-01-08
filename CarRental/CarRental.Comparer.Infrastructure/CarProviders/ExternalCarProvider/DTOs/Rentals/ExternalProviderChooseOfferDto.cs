namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Rentals;

public sealed record ExternalProviderChooseOfferDto(
	int OfferId,
	DateTime StartDate,
	string UserEmail
);
