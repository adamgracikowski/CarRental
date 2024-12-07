namespace CarRental.Comparer.API.DTOs.CarDetails;

public sealed record CarDetailsForEmployeeDto(
	string Model,
	string Make,
	int YearOfProduction,
	int Id
);