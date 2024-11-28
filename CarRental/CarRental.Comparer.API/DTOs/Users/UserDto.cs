namespace CarRental.Comparer.API.DTOs.Users;

public sealed record UserDto(
    string Email,
    string Name,
    string Lastname,
    int Age,
    int DrivingLicenseYears,
    double Longitude,
    double Latitude
);