namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Cars;

public sealed record CarDetailsDto(
    int ProductionYear,
    string FuelType,
    string TransmissionType,
    decimal Longitude,
    decimal Latitude
);