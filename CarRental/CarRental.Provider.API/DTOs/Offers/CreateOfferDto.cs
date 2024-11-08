namespace CarRental.Provider.API.DTOs.Offers;

public sealed record CreateOfferDto(
    int DrivingLicenseYears,
    int Age,
    decimal Latitude,
    decimal Longitude
);