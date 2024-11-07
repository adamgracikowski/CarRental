using CarRental.Common.Core.Enums;

namespace CarRental.Provider.API.Requests.Cars.DTOs;

public sealed record CarDetailsDto(
    int ProductionYear,
    FuelType FuelType,
    TransmissionType TransmissionType,
    decimal Longitude,
    decimal Latitude
);