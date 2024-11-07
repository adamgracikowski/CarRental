using CarRental.Provider.API.Requests.Insurances.DTOs;

namespace CarRental.Provider.API.Requests.Segments.DTOs;

public sealed record SegmentDto(
    string Name,
    string? Description,
    InsuranceDto Insurance
);