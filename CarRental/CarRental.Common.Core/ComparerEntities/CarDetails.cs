using CarRental.Common.Core.Enums;

namespace CarRental.Common.Core.ComparerEntities;

public sealed class CarDetails : EntityBase
{
    public string OuterId { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public string? Segment { get; set; }

    public FuelType FuelType { get; set; }

    public TransmissionType TransmissionType { get; set; }

    public int YearOfProduction { get; set; }

    public int NumberOfDoors { get; set; }

    public int NumberOfSeats { get; set; }

    public ICollection<RentalTransaction> RentalTransactions { get; set; }
}
