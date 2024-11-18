using CarRental.Comparer.Web.Requests.DTOs.Offers;
using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

namespace CarRental.Comparer.Web.Requests.OfferServices;

public interface IOfferService
{
	Task<OfferDto?> CreateOfferAsync(int providerId, int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken = default);

	Task<RentalIdDto?> ChooseOfferAsync(int providerId, string offerId, ChooseOfferDto chooseOfferDto, CancellationToken cancellationToken = default);
}
