using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Cars;

namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Models;

public sealed record ExternalModelWithCarsDto(
	ExternalModelDto Model,
	ICollection<ExternalCarDto> Cars
);