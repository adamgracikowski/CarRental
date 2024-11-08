using CarRental.Provider.API.DTOs.Insurances;

namespace CarRental.Provider.API.DTOs.Segments;

public sealed record SegmentDto(
    string Name,
    string? Description,
    InsuranceDto Insurance
);