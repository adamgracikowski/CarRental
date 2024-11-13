using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;

namespace CarRental.Comparer.Infrastructure.CarComparisons;

public interface ICarComparisonService
{
    Task<UnifiedCarListDto> GetAllAvailableCarsAsync(CancellationToken cancellationToken);
}