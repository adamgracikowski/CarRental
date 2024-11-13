using CarRental.Common.Core.Enums;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs;

public sealed record ModelDto(
    string Name,
    int NumberOfDoors,
    int NumberOfSeats,
    string EngineType,
    string WheelDriveType,
    ICollection<CarDto> Cars
);