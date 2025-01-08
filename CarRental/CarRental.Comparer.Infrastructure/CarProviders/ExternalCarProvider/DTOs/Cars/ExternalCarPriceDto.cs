namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Cars;

public sealed record ExternalCarPriceDto(
	int OfferId,
	decimal DailyPrice
);