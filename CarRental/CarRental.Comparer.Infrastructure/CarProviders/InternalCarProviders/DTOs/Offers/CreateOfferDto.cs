namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;

public sealed record CreateOfferDto(
    int DrivingLicenseYears,
    int Age,
    decimal Latitude,
    decimal Longitude
);