using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;

namespace CarRental.Comparer.Infrastructure.CarProviders;

public interface ICarProviderService
{
    Task<UnifiedCarListDto?> GetAvailableCarsAsync(CancellationToken cancellationToken);
}