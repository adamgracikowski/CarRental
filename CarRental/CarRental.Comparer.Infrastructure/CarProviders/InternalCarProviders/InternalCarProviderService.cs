using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.MultipartExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders;

public sealed class InternalCarProviderService : ICarProviderService
{
	private readonly HttpClient httpClient;
	private readonly InternalProviderOptions providerOptions;
	private readonly ILogger<InternalCarProviderService> logger;

	public string ProviderName => providerOptions.Name;

	public InternalCarProviderService(
		IHttpClientFactory httpClientFactory,
		IOptions<InternalProviderOptions> options,
		ILogger<InternalCarProviderService> logger)
	{
		this.providerOptions = options.Value;
		this.httpClient = httpClientFactory.CreateClient(providerOptions.Name);
		this.logger = logger;
	}

	public async Task<UnifiedCarListDto?> GetAvailableCarsAsync(CancellationToken cancellationToken)
	{
		try
		{
			var response = await this.httpClient.GetAsync("/Cars/Available");
			var providerCarListDto = await this.MapResponseAsync(response, cancellationToken);
			return providerCarListDto;
		}
		catch (Exception ex)
		{
			this.logger.LogInformation($"{ex.Message}");
			return null;
		}
	}

	public async Task<OfferDto?> CreateOfferAsync(int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken)
	{
		try
		{
			var response = await this.httpClient.PostAsJsonAsync($"/Cars/{carId}/Offers", createOfferDto, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				var offerDto = await response.Content.ReadFromJsonAsync<OfferDto>(cancellationToken);
				return offerDto;
			}
			else
			{
				var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
				this.logger.LogInformation($"Error: {errorMessage}");
				return null;
			}
		}
		catch (Exception ex)
		{
			this.logger.LogInformation(ex.Message);
			return null;
		}
	}

	public async Task<RentalIdWithDateTimesDto?> ChooseOfferAsync(int offerId, ProviderChooseOfferDto providerChooseOfferDto, CancellationToken cancellationToken)
	{
		try
		{
			var response = await this.httpClient.PostAsJsonAsync($"/Offers/{offerId}", providerChooseOfferDto, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				var internalRentalIdWithDateTimesDto = await response.Content.ReadFromJsonAsync<InternalRentalIdWithDateTimesDto>(cancellationToken);

				if (internalRentalIdWithDateTimesDto is null)
				{
					this.logger.LogWarning("internal id is null");
					return null;
				}

				var rentalIdWithDateTimesDto = new RentalIdWithDateTimesDto(
					internalRentalIdWithDateTimesDto.id.ToString(),
					internalRentalIdWithDateTimesDto.GeneratedAt,
					internalRentalIdWithDateTimesDto.ExpiresAt);

				return rentalIdWithDateTimesDto;
			}
			else
			{
				var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
				this.logger.LogInformation($"Error: {errorMessage}");
				return null;
			}
		}
		catch (Exception ex)
		{
			this.logger.LogInformation(ex.Message);
			return null;
		}
	}

	public async Task<RentalStatusDto?> GetRentalStatusByIdAsync(string rentalId, CancellationToken cancellationToken)
	{
		try
		{
			if (int.TryParse(rentalId, out var internalRentalId))
			{
				this.logger.LogWarning("rentalId is not a number, parse failed");
			}

			var response = await this.httpClient.GetAsync($"/Rentals/{internalRentalId}/status", cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				var internalRentalStatusDto = await response.Content.ReadFromJsonAsync<InternalRentalStatusDto>(cancellationToken);

				if (internalRentalStatusDto is null)
				{
					this.logger.LogWarning("internalRentalStatusDto is null");
					return null;
				}

				var rentalStatusDto = new RentalStatusDto(internalRentalStatusDto.id.ToString(), internalRentalStatusDto.status);
				return rentalStatusDto;
			}
			else
			{
				var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
				this.logger.LogInformation($"Error: {errorMessage}");
				return null;
			}
		}
		catch (Exception ex)
		{
			this.logger.LogInformation(ex.Message);
			return null;
		}
	}

	public async Task<RentalReturnDto?> AcceptRentalReturnAsync(string rentalId, AcceptRentalReturnDto acceptRentalReturnDto, CancellationToken cancellationToken)
	{
		try
		{
			var response = await this.httpClient.PostAsync($"/Rentals/{rentalId}/accept-return", acceptRentalReturnDto.ToMultipartFormDataContent(), cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				var internalRentalStatusDto = await response.Content.ReadFromJsonAsync<RentalReturnDto>(cancellationToken);

				if (internalRentalStatusDto is null)
				{
					this.logger.LogWarning("RentalReturnDto is null");
					return null;
				}

				return internalRentalStatusDto;
			}
			else
			{
				var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
				this.logger.LogInformation($"Error: {errorMessage}");
				return null;
			}
		}
		catch (Exception ex)
		{
			this.logger.LogInformation(ex.Message);
			return null;
		}
	}

	private async Task<UnifiedCarListDto?> MapResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
	{
		var carListDto = await response.Content.ReadFromJsonAsync<CarListDto>(cancellationToken);

		if (carListDto is null)
		{
			return null;
		}

		var providerMakesDto = carListDto.Makes.Select(mk =>
		{
			var models = mk.Models.Select(m =>
			{
				var cars = m.Cars
					.Select(c => new UnifiedCarDto(c.Id.ToString(), c.ProductionYear, providerOptions.Name))
					.ToList();

				var model = new UnifiedModelDto(
					Name: m.Name,
					NumberOfDoors: m.NumberOfDoors,
					NumberOfSeats: m.NumberOfSeats,
					EngineType: m.EngineType,
					WheelDriveType: m.WheelDriveType,
					Cars: cars
				);

				return model;
			}).ToList();

			var make = new UnifiedMakeDto(
				Name: mk.Name,
				Models: models,
				LogoUrl: string.Empty
			);

			return make;
		}).ToList();

		return new UnifiedCarListDto(providerMakesDto);
	}
}