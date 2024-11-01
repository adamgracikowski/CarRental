using CarRental.Common.Core.Enums;

namespace CarRental.Common.Core.ProviderEntities;

public sealed class Model : EntityBase
{
    public required string Name { get; set; }

    public required Make Make { get; set; }

    public int MakeId { get; set; }

    public required Segment Segment { get; set; }

    public int SegmentId { get; set; }

    public int NumberOfDoors { get; set; }

    public int NumberOfSeats { get; set; }

    public EngineType EngineType { get; set; }

    public WheelDriveType WheelDriveType { get; set; }

    public ICollection<Car> Cars { get; set; } = [];
}