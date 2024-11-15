using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;

namespace CarRental.Comparer.Infrastructure.CarComparisons;

public interface ICarComparisonService
{
    Task<UnifiedCarListDto> GetAllAvailableCarsAsync(CancellationToken cancellationToken);

    Task<OfferDto?> CreateOfferAsync(string providerName, int carsId, CreateOfferDto createOfferDto, CancellationToken cancellationToken);

    Task<RentalIdDto?> ChooseOfferAsync(string providerName, int offerId, ChooseOfferDto chooseOfferDto, CancellationToken cancellationToken);
}