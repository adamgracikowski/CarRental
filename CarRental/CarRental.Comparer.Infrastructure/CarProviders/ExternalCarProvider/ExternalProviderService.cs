using Azure;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Segments.Insurances;
using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Makes;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider;

public sealed class ExternalProviderService : ICarProviderService
{
	private readonly HttpClient httpClient;
	private readonly ExternalProviderOptions providerOptions;
	private readonly ILogger<ExternalProviderService> logger;
	public string ProviderName => providerOptions.Name;

	public ExternalProviderService(
		IHttpClientFactory httpClientFactory,
		IOptions<ExternalProviderOptions> options,
		ILogger<ExternalProviderService> logger)
	{
		this.providerOptions = options.Value;
		this.httpClient = httpClientFactory.CreateClient(providerOptions.Name);
		this.logger = logger;
	}

	public Task<RentalReturnDto?> AcceptRentalReturnAsync(string rentalId, AcceptRentalReturnDto acceptRentalReturnDto, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<RentalIdWithDateTimesDto?> ChooseOfferAsync(int offerId, ProviderChooseOfferDto providerChooseOfferDto, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public async Task<OfferDto?> CreateOfferAsync(int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken = default)
	{
		try
		{
			var priceResponse = await this.httpClient.GetAsync($"/cars/{carId}/price?licenseYears={createOfferDto.DrivingLicenseYears}");

			if (!priceResponse.IsSuccessStatusCode)
			{
				var errorMessage = await priceResponse.Content.ReadAsStringAsync(cancellationToken);
				this.logger.LogInformation($"Error: {errorMessage}");
				return null;
			}

			var carResponse = await this.httpClient.GetAsync($"cars?carId={carId}");

			if (!carResponse.IsSuccessStatusCode)
			{
				var errorMessage = await carResponse.Content.ReadAsStringAsync(cancellationToken);
				this.logger.LogInformation($"Error: {errorMessage}");
				return null;
			}

			var priceDto = await priceResponse.Content.ReadFromJsonAsync<ExternalCarPriceDto>(cancellationToken);
			Console.WriteLine(await carResponse.Content.ReadAsStringAsync());
			var externalCarDetailsDto = await carResponse.Content.ReadFromJsonAsync<List<ExternalCarDetailsDto>>(cancellationToken);

			var carDetailsDto = new CarDetailsDto(
				externalCarDetailsDto[0].Year,
				externalCarDetailsDto[0].FuelType.ToString(), //todo - czemu to jest cyfra nooo
				externalCarDetailsDto[0].GearboxType.ToString(), //todo - przerobić na nazwe normalną
				0, //todo - nie ma lokalizacji...
				0
			);

			return new OfferDto(
				priceDto.OfferId,
				externalCarDetailsDto[0].RentalCost,
				priceDto.DailyPrice - externalCarDetailsDto[0].RentalCost,
				carDetailsDto,
				null
			);
		}
		catch (Exception ex)
		{
			this.logger.LogInformation($"Error: {ex.Message}");
			return null;
		}
	}

	public async Task<UnifiedCarListDto?> GetAvailableCarsAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await this.httpClient.GetAsync("/cars/brandsModels");
			if (response.IsSuccessStatusCode)
			{
				var externalMakeList = await response.Content.ReadFromJsonAsync<List<ExternalMakeWithModelsWithCarsDto>>(cancellationToken);

				if (externalMakeList is null)
				{
					this.logger.LogWarning("ExternalMakeWithModelsWithCarsDto list is null");
					return null;
				}

				var unifiedExternalMakeList = this.MapAvaliableCarsResponse(externalMakeList, cancellationToken);

				return unifiedExternalMakeList;
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
			this.logger.LogInformation($"{ex.Message}");
			return null;
		}
	}

	public async Task<RentalStatusDto?> GetRentalStatusByIdAsync(string rentalId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public async Task<RentalStatusDto?> ReturnRentalAsync(string rentalId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	private UnifiedCarListDto MapAvaliableCarsResponse(List<ExternalMakeWithModelsWithCarsDto> externalMakeWithModelsWithCarsDtos, CancellationToken cancellationToken)
	{
		var providerMakesDto = externalMakeWithModelsWithCarsDtos.Select(mk =>
		{
			var models = mk.ModelCars.Select(m =>
			{
				var cars = m.Cars
					.Select(c => new UnifiedCarDto(c.Id.ToString(), c.Year, providerOptions.Name))
					.ToList();

				var model = new UnifiedModelDto(
					Name: m.Model.ModelName,
					NumberOfDoors: m.Model.DoorCount,
					NumberOfSeats: m.Model.SeatCount,
					EngineType: m.Model.EngineType,
					WheelDriveType: "other",
					Cars: cars
				);

				return model;
			}).ToList();

			var make = new UnifiedMakeDto(
				Name: mk.Brand.BrandName,
				Models: models,
				LogoUrl: string.Empty
			);

			return make;
		}).ToList();

		return new UnifiedCarListDto(providerMakesDto);
	}
}