using Azure;
using CarRental.Common.Core.Enums;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Segments.Insurances;
using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Makes;
using CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.RentalStatusConversions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;

namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider;

public sealed class ExternalProviderService : ICarProviderService
{
	private readonly HttpClient httpClient;
	private readonly ExternalProviderOptions providerOptions;
	private readonly ILogger<ExternalProviderService> logger;
	private readonly IRentalStatusConverter rentalStatusConverter;
	private readonly IDateTimeProvider dateTimeProvider;
	public string ProviderName => providerOptions.Name;

	public ExternalProviderService(
		IHttpClientFactory httpClientFactory,
		IOptions<ExternalProviderOptions> options,
		ILogger<ExternalProviderService> logger,
		IRentalStatusConverter rentalStatusConverter,
		IDateTimeProvider dateTimeProvider)
	{
		this.providerOptions = options.Value;
		this.httpClient = httpClientFactory.CreateClient(providerOptions.Name);
		this.logger = logger;
		this.rentalStatusConverter = rentalStatusConverter;
		this.dateTimeProvider = dateTimeProvider;
	}

	public Task<RentalReturnDto?> AcceptRentalReturnAsync(string rentalId, AcceptRentalReturnDto acceptRentalReturnDto, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public async Task<RentalIdWithDateTimesDto?> ChooseOfferAsync(int offerId, ProviderChooseOfferDto providerChooseOfferDto, CancellationToken cancellationToken = default)
	{
		try
		{
			var startDate = dateTimeProvider.UtcNow;
			var endDate = dateTimeProvider.UtcNow.AddMinutes(15);

			var externalProviderChooseOfferDto = new ExternalProviderChooseOfferDto(offerId, startDate, providerChooseOfferDto.EmailAddress);

			var response = await this.httpClient.PostAsJsonAsync($"/rentals", externalProviderChooseOfferDto, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				int rentalId = await response.Content.ReadFromJsonAsync<int>(cancellationToken);

				if (rentalId == 0)
				{
					this.logger.LogWarning("externalinternal id is 0");
					return null;
				}

				var rentalIdWithDateTimesDto = new RentalIdWithDateTimesDto(
					rentalId.ToString(),
					startDate,
					endDate);

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
				externalCarDetailsDto[0].FuelType, 
				externalCarDetailsDto[0].GearboxType,
				0,
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
		try
		{
			if (int.TryParse(rentalId, out int rentalIdInt) == false)
			{
				this.logger.LogWarning($"RentalId {rentalId} is not a number");
				return null;
			}

			var response = await this.httpClient.GetAsync($"/rentals/{rentalId}", cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				var externalRentalStatusDto = await response.Content.ReadFromJsonAsync<ExternalRentalStatusDto>(cancellationToken);

				if (externalRentalStatusDto is null)
				{
					this.logger.LogWarning("externalRentalStatusDto is null");
					return null;
				}
				
				var rentalStatusDto = new RentalStatusDto(externalRentalStatusDto.RentalId.ToString(), externalRentalStatusDto.CarState);
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

	public async Task<RentalStatusDto?> ReturnRentalAsync(string rentalId, CancellationToken cancellationToken = default)
	{
		try
		{
			if (int.TryParse(rentalId, out int rentalIdInt) == false)
			{
				this.logger.LogWarning($"RentalId {rentalId} is not a number");
				return null;
			}

			var response = await this.httpClient.PostAsync($"/rentals/{rentalId}/return", null, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				var rentalStatusDto = new RentalStatusDto(rentalId, RentalStatus.ReadyForReturn.ToString()); 
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