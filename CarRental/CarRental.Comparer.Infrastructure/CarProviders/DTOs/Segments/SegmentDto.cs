using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Segments.Insurances;

namespace CarRental.Comparer.Infrastructure.CarProviders.DTOs.Segments;

public sealed record SegmentDto(
	string Name,
	string? Description,
	InsuranceDto Insurance
);