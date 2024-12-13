using CarRental.Comparer.Web.Requests.DTOs.Offers;
using CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.OfferServices;

public sealed class OfferService : IOfferService
{
	public static readonly string ClientWithoutAuthorization = "ClientWithoutAuthorization";
	public static readonly string ClientWithAuthorization = "ClientWithAuthorization";

	private readonly HttpClient httpClientWithoutAuthorization;
	private readonly HttpClient httpClientWithAuthorization;

	public OfferService(IHttpClientFactory httpClientFactory)
	{
		this.httpClientWithAuthorization = httpClientFactory.CreateClient(ClientWithAuthorization);
		this.httpClientWithoutAuthorization = httpClientFactory.CreateClient(ClientWithoutAuthorization);
	}

	public async Task<OfferDto?> CreateOfferAsync(
		int providerId,
		int carId,
		CreateOfferDto createOfferDto,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var url = $"providers/{providerId}/cars/{carId}/offers";
			var response = await httpClientWithoutAuthorization.PostAsJsonAsync(url, createOfferDto, cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var offerDto = await response.Content.ReadFromJsonAsync<OfferDto>(cancellationToken);

			return offerDto;
		}
		catch (Exception ex)
		{
			return null;
		}
	}

	public async Task<RentalTransactionIdWithDateTimesDto?> ChooseOfferAsync(
		int providerId,
		string offerId,
		ChooseOfferDto chooseOfferDto,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var url = $"/providers/{providerId}/offers/{offerId}";
			var response = await httpClientWithAuthorization.PostAsJsonAsync(url, chooseOfferDto, cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var rentalIdWithDateTimesDto = await response.Content.ReadFromJsonAsync<RentalTransactionIdWithDateTimesDto>(cancellationToken);

			return rentalIdWithDateTimesDto;
		}
		catch (Exception)
		{
			return null;
		}
	}
}