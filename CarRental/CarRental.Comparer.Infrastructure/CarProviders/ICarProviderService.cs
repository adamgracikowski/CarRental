using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;

namespace CarRental.Comparer.Infrastructure.CarProviders;

public interface ICarProviderService
{
    string ProviderName { get; }

    Task<UnifiedCarListDto?> GetAvailableCarsAsync(CancellationToken cancellationToken);

    Task<OfferDto?> CreateOfferAsync(int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken);

    Task<RentalIdWithDateTimesDto?> ChooseOfferAsync(int offerId, ProviderChooseOfferDto providerChooseOfferDto, CancellationToken cancellationToken);

    Task<RentalStatusDto?> GetRentalStatusByIdAsync(string rentalId, CancellationToken cancellationToken);
}