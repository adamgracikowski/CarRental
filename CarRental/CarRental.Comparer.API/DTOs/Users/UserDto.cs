namespace CarRental.Comparer.API.DTOs.Users;

public sealed record UserDto(
	string Email,
	string Name,
	string Lastname,
	DateTime Birthday,
	DateTime DrivingLicenseDate,
	double Longitude,
	double Latitude
);