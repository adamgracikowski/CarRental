namespace CarRental.Comparer.Web.Requests.DTOs.Cars;

public sealed record CarDisplayDto
{
    public string MakeName { get; set; }

    public string ModelName { get; set; }

    public int NumberOfDoors { get; set; }

    public int NumberOfSeats { get; set; }

    public string EngineType { get; set; }

    public string WheelDriveType { get; set; }
}