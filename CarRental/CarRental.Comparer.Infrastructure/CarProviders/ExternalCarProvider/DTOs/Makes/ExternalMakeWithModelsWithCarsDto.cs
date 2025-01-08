using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Models;

namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Makes;
public sealed record ExternalMakeWithModelsWithCarsDto(
	ExternalMakeDto Brand,
	ICollection<ExternalModelWithCarsDto> ModelCars
);