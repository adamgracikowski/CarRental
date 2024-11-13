namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs;

public sealed record UnifiedMakeDto(
    string Name, 
    ICollection<UnifiedModelDto> Models,
    string LogoUrl
);