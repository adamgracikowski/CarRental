using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Segments;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;

public sealed record OfferDto(
    int Id,
    decimal RentalPricePerDay,
    decimal InsurancePricePerDay,
    DateTime GeneratedAt,
    DateTime ExpiresAt,
    CarDetailsDto Car,
    SegmentDto Segment
);