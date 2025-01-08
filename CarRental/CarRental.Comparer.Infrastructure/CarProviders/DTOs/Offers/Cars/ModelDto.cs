namespace CarRental.Comparer.Infrastructure.CarProviders.DTOs.Offers.Cars;

public sealed record ModelDto(
    string Name,
    int NumberOfDoors,
    int NumberOfSeats,
    string EngineType,
    string WheelDriveType,
    ICollection<CarDto> Cars
);