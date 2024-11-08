namespace CarRental.Provider.API.DTOs.RentalReturns;

public sealed record AcceptRentalReturnDto(
    string? Description,
    IFormFile? Image,
    decimal Latitude,
    decimal Longitude
);