using CarRental.Common.Core.ProviderEntities;
using CarRental.Comparer.Infrastructure.Cache;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
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
        providerOptions = options.Value;
        httpClient = httpClientFactory.CreateClient(providerOptions.Name);
        this.logger = logger;
    }

    public async Task<UnifiedCarListDto?> GetAvailableCarsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.GetAsync("/Cars/Available");
            var providerCarListDto = await MapResponseAsync(response, cancellationToken);
            return providerCarListDto;
        }
        catch (Exception ex)
        {
            logger.LogInformation($"{ex.Message}");
            return null;
        }
    }

    public async Task<OfferDto?> CreateOfferAsync(int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync($"/Cars/{carId}/Offers", createOfferDto, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var offerDto = await response.Content.ReadFromJsonAsync<OfferDto>(cancellationToken);
                return offerDto;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                logger.LogInformation($"Error: {errorMessage}");
                return null;
            }
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);
            return null;
        }
    }

    public async Task<RentalIdDto?> ChooseOfferAsync(int offerId, ProviderChooseOfferDto providerChooseOfferDto, CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync($"/Offers/{offerId}", providerChooseOfferDto, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var rentalIdDto = await response.Content.ReadFromJsonAsync<RentalIdDto>(cancellationToken);
                return rentalIdDto;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                logger.LogInformation($"Error: {errorMessage}");
                return null;
            }
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);
            return null;
        }
    }

    public async Task<RentalStatusDto?> GetRentalStatusByIdAsync(int rentalId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.GetAsync($"/Rentals/{rentalId}/status", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var rentalStatusDto = await response.Content.ReadFromJsonAsync<RentalStatusDto>(cancellationToken);
                return rentalStatusDto;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                logger.LogInformation($"Error: {errorMessage}");
                return null;
            }
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);
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