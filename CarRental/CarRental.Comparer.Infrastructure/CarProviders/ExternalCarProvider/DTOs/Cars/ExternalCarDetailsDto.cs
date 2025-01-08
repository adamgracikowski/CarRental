using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Makes;
using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Models;

namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Cars;

public sealed record ExternalCarDetailsDto(
	int CarId,
	ExternalMakeDto Brand,
	ExternalModelDto Model,
	int Year,
	int FuelType,
	int GearboxType,
	string Location,
	decimal RentalCost,
	decimal InsuranceCost
);