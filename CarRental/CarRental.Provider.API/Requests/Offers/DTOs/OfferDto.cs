using CarRental.Provider.API.Requests.Cars.DTOs;
using CarRental.Provider.API.Requests.Segments.DTOs;

namespace CarRental.Provider.API.Requests.Offers.DTOs;

public sealed record OfferDto(
    int Id,
    decimal RentalPricePerDay,
    decimal InsurancePricePerDay,
    DateTime GeneratedAt,
    DateTime ExpiresAt,
    CarDetailsDto Car,
    SegmentDto Segment
);