using CarRental.Comparer.Web.Requests.DTOs.Insurances;

namespace CarRental.Comparer.Web.Requests.DTOs.Segments;

public sealed record SegmentDto(
	string Name,
	string? Description,
	InsuranceDto Insurance
);
