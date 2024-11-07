namespace CarRental.Comparer.API.Requests.Users.DTOs;

public sealed record class CreateUserDto
{
    public string Email { get; set; }

    public string Name { get; set; }

    public string Lastname { get; set; }

    public int Age { get; set; }

    public int DrivingLicenseYears { get; set; }

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }
}
