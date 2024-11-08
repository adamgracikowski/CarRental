using CarRental.Provider.API.DTOs.Cars;
using CarRental.Provider.API.DTOs.Segments;

namespace CarRental.Provider.API.DTOs.Offers;

public sealed record OfferDto(
    int Id,
    decimal RentalPricePerDay,
    decimal InsurancePricePerDay,
    DateTime GeneratedAt,
    DateTime ExpiresAt,
    CarDetailsDto Car,
    SegmentDto Segment
);