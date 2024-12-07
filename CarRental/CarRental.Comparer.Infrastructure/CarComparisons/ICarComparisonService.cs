using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;

namespace CarRental.Comparer.Infrastructure.CarComparisons;

public interface ICarComparisonService
{
	Task<UnifiedCarListDto> GetAllAvailableCarsAsync(CancellationToken cancellationToken);

	Task<OfferDto?> CreateOfferAsync(string providerName, int carsId, CreateOfferDto createOfferDto, CancellationToken cancellationToken);

	Task<RentalIdWithDateTimesDto?> ChooseOfferAsync(string providerName, int offerId, ProviderChooseOfferDto providerChooseOfferDto, CancellationToken cancellationToken);

	Task<RentalStatusDto?> GetRentalStatusByIdAsync(string providerName, string rentalId, CancellationToken cancellationToken);

	Task<RentalReturnDto?> AcceptRentalReturnAsync(string providerName, string rentalId, AcceptRentalReturnDto acceptRentalReturnDto, CancellationToken cancellationToken);
}