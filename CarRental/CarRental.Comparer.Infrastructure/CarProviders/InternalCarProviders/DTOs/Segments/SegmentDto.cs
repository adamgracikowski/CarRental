using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Insurances;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Segments;

public sealed record SegmentDto(
    string Name,
    string? Description,
    InsuranceDto Insurance
);