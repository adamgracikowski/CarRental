namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;

public sealed record ChooseOfferDto(
    string EmailAddress, 
    decimal RentalPricePerDay,
    decimal InsurancePricePerDay,
    string CarOuterId,
    string Make,
    string Model,
    string? Segment,
    string? FuelType,
    string? TransmissionType,
    int YearOfProduction,
    int? NumberOfDoors,
    int? NumberOfSeats
);