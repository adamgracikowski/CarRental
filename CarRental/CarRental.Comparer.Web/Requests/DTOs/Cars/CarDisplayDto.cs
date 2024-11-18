namespace CarRental.Comparer.Web.Requests.DTOs.Cars;

public sealed record CarDisplayDto(
	string MakeName,
	string ModelName,
	int NumberOfDoors,
	int NumberOfSeats,
	string? EngineType,
	string? WheelDriveType
);