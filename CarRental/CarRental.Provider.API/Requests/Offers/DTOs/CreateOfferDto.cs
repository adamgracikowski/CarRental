namespace CarRental.Provider.API.Requests.Offers.DTOs;

public sealed record CreateOfferDto(
    int DrivingLicenseYears,
    int Age,
    decimal Latitude,
    decimal Longitude
);