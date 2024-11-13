namespace CarRental.Comparer.API.DTOs.Users;

public sealed record class CreateUserDto(
    string Email,
    string Name,
    string Lastname,
    int Age,
    int DrivingLicenseYears,
    decimal Longitude,
    decimal Latitude
);