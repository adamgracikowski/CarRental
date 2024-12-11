namespace CarRental.Comparer.Web.Requests.DTOs.Cars;

public sealed record CarDetailsForEmployeeDto(
	string Model,
	string Make,
	int YearOfProduction,
	int Id
);
