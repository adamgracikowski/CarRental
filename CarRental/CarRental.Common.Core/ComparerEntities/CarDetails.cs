namespace CarRental.Common.Core.ComparerEntities;

public sealed class CarDetails : EntityBase
{
    public required string OuterId { get; set; }

    public required string Make { get; set; }

    public required string Model { get; set; }

    public string? Segment { get; set; }

    public string? FuelType { get; set; }

    public string? TransmissionType { get; set; }

    public int YearOfProduction { get; set; }

    public int? NumberOfDoors { get; set; }

    public int? NumberOfSeats { get; set; }

    public ICollection<RentalTransaction> RentalTransactions { get; set; } = [];
}