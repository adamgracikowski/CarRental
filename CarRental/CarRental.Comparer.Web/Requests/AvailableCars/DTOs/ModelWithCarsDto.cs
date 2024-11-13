namespace CarRental.Comparer.Web.Requests.AvailableCars.DTOs;

public sealed record ModelWithCarsDto
{
    public string Name { get; set; }

    public int NumberOfDoors { get; set; }

    public int NumberOfSeats { get; set; }

    public string EngineType { get; set; }

    public string WheelDriveType { get; set; }

    public ICollection<CarIdProductionYearDto> Cars { get; set; }
}
