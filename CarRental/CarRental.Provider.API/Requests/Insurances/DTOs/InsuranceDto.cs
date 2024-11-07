namespace CarRental.Provider.API.Requests.Insurances.DTOs;

public sealed record InsuranceDto(
    string Name,
    string? Description
);