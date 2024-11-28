using CarRental.Comparer.Web.Requests.DTOs.Offers;
using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.OfferServices;

public sealed class OfferService : IOfferService
{
	private readonly HttpClient httpClient;

	public OfferService(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<OfferDto?> CreateOfferAsync(
		int providerId,
		int carId,
		CreateOfferDto createOfferDto,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var url = $"Providers/{providerId}/Cars/{carId}/Offers";
			var response = await httpClient.PostAsJsonAsync(url, createOfferDto, cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var offerDto = await response.Content.ReadFromJsonAsync<OfferDto>(cancellationToken);

			return offerDto;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public async Task<RentalTransactionIdDto?> ChooseOfferAsync(
		int providerId,
		string offerId,
		ChooseOfferDto chooseOfferDto,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var url = $"/Providers/{providerId}/Offers/{offerId}";
			var response = await httpClient.PostAsJsonAsync(url, chooseOfferDto, cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var rentalIdDto = await response.Content.ReadFromJsonAsync<RentalTransactionIdDto>(cancellationToken);

			return rentalIdDto;
		}
		catch (Exception)
		{
			return null;
		}
	}
}
