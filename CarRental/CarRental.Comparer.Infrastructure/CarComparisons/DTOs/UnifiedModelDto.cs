namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs;

public sealed record UnifiedModelDto(
    string Name,
    int NumberOfDoors,
    int NumberOfSeats,
    string EngineType,
    string WheelDriveType,
    ICollection<UnifiedCarDto> Cars
);