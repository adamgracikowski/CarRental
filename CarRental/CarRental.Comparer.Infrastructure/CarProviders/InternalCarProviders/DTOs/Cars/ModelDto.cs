namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Cars;

public sealed record ModelDto(
    string Name,
    int NumberOfDoors,
    int NumberOfSeats,
    string EngineType,
    string WheelDriveType,
    ICollection<CarDto> Cars
);