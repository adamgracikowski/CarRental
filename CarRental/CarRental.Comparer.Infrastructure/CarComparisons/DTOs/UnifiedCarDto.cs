namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs;

public sealed record UnifiedCarDto(
    string Id,
    int ProductionYear,
    string ProviderName
);