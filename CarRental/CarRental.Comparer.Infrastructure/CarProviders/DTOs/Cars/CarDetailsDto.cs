namespace CarRental.Comparer.Infrastructure.CarProviders.DTOs.Cars;

public sealed record CarDetailsDto(
	int ProductionYear,
	string FuelType,
	string TransmissionType,
	decimal Longitude,
	decimal Latitude
);