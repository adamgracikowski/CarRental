using CarRental.Common.Core.Enums;

namespace CarRental.Provider.API.DTOs.Cars;

public sealed record CarDetailsDto(
    int ProductionYear,
    FuelType FuelType,
    TransmissionType TransmissionType,
    decimal Longitude,
    decimal Latitude
);