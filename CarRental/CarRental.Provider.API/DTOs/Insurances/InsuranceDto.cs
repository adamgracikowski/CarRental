namespace CarRental.Provider.API.DTOs.Insurances;

public sealed record InsuranceDto(
    string Name,
    string? Description
);