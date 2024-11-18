using CarRental.Comparer.Web.Requests.DTOs.Cars;

namespace CarRental.Comparer.Web.Requests.DTOs.Models;

public record ModelDto(
	string Name,
	int NumberOfDoors,
	int NumberOfSeats,
	string? EngineType,
	string? WheelDriveType,
	ICollection<CarDto> Cars
);