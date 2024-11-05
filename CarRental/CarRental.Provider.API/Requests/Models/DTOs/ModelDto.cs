using CarRental.Common.Core.Enums;
using CarRental.Provider.API.Requests.Cars.DTOs;

namespace CarRental.Provider.API.Requests.Models.DTOs;

public sealed record ModelDto(
    string Name,
    int NumberOfDoors,
    int NumberOfSeats,
    EngineType EngineType,
    WheelDriveType WheelDriveType,
    ICollection<CarDto> Cars
);