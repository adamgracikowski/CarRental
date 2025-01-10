namespace CarRental.Comparer.Web.Requests.DTOs.Cars;

public sealed record CarDetailsDto(
	int ProductionYear,
	string? FuelType,
	string? TransmissionType,
	decimal Longitude,
	decimal Latitude,
	string? Address
);