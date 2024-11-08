using CarRental.Common.Core.Enums;
using CarRental.Provider.API.DTOs.Cars;

namespace CarRental.Provider.API.DTOs.Models;

public sealed record ModelDto(
    string Name,
    int NumberOfDoors,
    int NumberOfSeats,
    EngineType EngineType,
    WheelDriveType WheelDriveType,
    ICollection<CarDto> Cars
);