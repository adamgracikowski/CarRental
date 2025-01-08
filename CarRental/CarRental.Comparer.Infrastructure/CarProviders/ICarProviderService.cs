using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;

namespace CarRental.Comparer.Infrastructure.CarProviders;

public interface ICarProviderService
{
	string ProviderName { get; }

	Task<UnifiedCarListDto?> GetAvailableCarsAsync(CancellationToken cancellationToken = default);

	Task<OfferDto?> CreateOfferAsync(int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken = default);

	Task<RentalIdWithDateTimesDto?> ChooseOfferAsync(int offerId, ProviderChooseOfferDto providerChooseOfferDto, CancellationToken cancellationToken = default);

	Task<RentalStatusDto?> GetRentalStatusByIdAsync(string rentalId, CancellationToken cancellationToken = default);

	Task<RentalReturnDto?> AcceptRentalReturnAsync(string rentalId, AcceptRentalReturnDto acceptRentalReturnDto, CancellationToken cancellationToken = default);

	Task<RentalStatusDto?> ReturnRentalAsync(string rentalId, CancellationToken cancellationToken = default);
}