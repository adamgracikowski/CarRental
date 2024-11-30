using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Infrastructure.Storages.BlobStorage;
using CarRental.Comparer.Infrastructure.Cache;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using CarRental.Comparer.Infrastructure.CarProviders;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarRental.Comparer.Infrastructure.CarComparisons;

public sealed class CarComparisonService : ICarComparisonService
{
    private readonly IEnumerable<ICarProviderService> carProviderServices;
    private readonly IBlobStorageService blobStorageService;
    private readonly BlobContainersOptions options;
    private readonly string StorageAccountName = string.Empty;
    private readonly ILogger<CarComparisonService> logger;
    private readonly ICacheKeyGenerator keyGenerator;
    private readonly ICacheService cacheService;

    public CarComparisonService(
        IEnumerable<ICarProviderService> carProviderServices,
        IBlobStorageService blobStorageService,
        IOptions<BlobContainersOptions> options,
        IConfiguration configuration,
        IRepositoryBase<Provider> providersRepository,
        ILogger<CarComparisonService> logger,
        ICacheKeyGenerator keyGenerator,
        ICacheService cacheService)
    {
        this.carProviderServices = carProviderServices;
        this.blobStorageService = blobStorageService;
        this.options = options.Value;
        this.logger = logger;
        StorageAccountName = configuration.GetValue<string>($"AzureBlobStorage:StorageAccountName") ?? string.Empty;
        this.keyGenerator = keyGenerator;
        this.cacheService = cacheService;
    }

    public async Task<UnifiedCarListDto> GetAllAvailableCarsAsync(CancellationToken cancellationToken)
    {
        var carsKey = this.keyGenerator.GenerateCarsKey();

        var cacheResult = await this.cacheService.GetDataByKeyAsync<UnifiedCarListDto>(carsKey);

        if (cacheResult != null && cacheResult.Makes.Count > 0)
        {
            return cacheResult;
        }

        var tasks = this.carProviderServices.Select(provider => provider.GetAvailableCarsAsync(cancellationToken));
        
        var results = await Task.WhenAll(tasks);

        var aggregatedResponse = await this.AggregateResponsesAsync(results, cancellationToken);

        await this.cacheService.AddDataAsync(carsKey, aggregatedResponse);

        return aggregatedResponse;
    }

    public async Task<OfferDto?> CreateOfferAsync(string providerName, int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken)
    {
        var carProviderService = GetCarProviderServiceByName(providerName);

        if (carProviderService is null) return null;

        var offer = await carProviderService.CreateOfferAsync(carId, createOfferDto, cancellationToken);

        return offer;
    }


    public async Task<RentalIdWithDateTimesDto?> ChooseOfferAsync(string providerName, int offerId, ProviderChooseOfferDto providerChooseOfferDto, CancellationToken cancellationToken)
    {
        var carProviderService = this.GetCarProviderServiceByName(providerName);

        if (carProviderService is null) return null;

        var rentalIdWithDateTimesDto = await carProviderService.ChooseOfferAsync(offerId, providerChooseOfferDto, cancellationToken);

        return rentalIdWithDateTimesDto; 
    }

    public async Task<RentalStatusDto?> GetRentalStatusByIdAsync(string providerName, string rentalId, CancellationToken cancellationToken)
    {
        var carProviderService = this.GetCarProviderServiceByName(providerName);

        if (carProviderService is null) return null;

        var rentalStatusDto = await carProviderService.GetRentalStatusByIdAsync(rentalId, cancellationToken);

        return rentalStatusDto; 
    }

    private ICarProviderService? GetCarProviderServiceByName(string name)
    {
        var carProviderService = this.carProviderServices.FirstOrDefault(providerService => providerService.ProviderName == name);

        if (carProviderService is null)
        {
            this.logger.LogWarning($"No service found for provider with name: '{name}'.");
            return null;
        }

        return carProviderService;
    }

    private async Task<UnifiedCarListDto> AggregateResponsesAsync(IEnumerable<UnifiedCarListDto?> responses, CancellationToken cancellationToken)
    {
        var files = await this.blobStorageService.GetFilesAsync(options.MakeLogosContainer, cancellationToken);

        var dictionary = files.ToDictionary(
            f => Path.GetFileNameWithoutExtension(f),
            f => $"https://{StorageAccountName}.blob.core.windows.net/{options.MakeLogosContainer}/{f}"
        );

        var placeholder = "placeholder";

        if (!dictionary.TryGetValue(placeholder, out var placeholderUrl) || placeholderUrl is null)
        {
            placeholderUrl = string.Empty;
        }

        var makes = responses
            .Where(r => r is not null)
            .SelectMany(r => r!.Makes)
            .Select(mk =>
            {
                var models = mk.Models
                    .GroupBy(m => m.Name)
                    .Select(m =>
                    {
                        var first = m.First();
                        var model = new UnifiedModelDto(
                            Name: first.Name,
                            NumberOfDoors: first.NumberOfDoors,
                            NumberOfSeats: first.NumberOfSeats,
                            EngineType: first.EngineType,
                            WheelDriveType: first.WheelDriveType,
                            Cars: m.SelectMany(m => m.Cars).ToList()
                        );

                        return model;
                    }).ToList();

                var makeKey = mk.Name
                    .Replace(' ', '-')
                    .ToLower();

                if (!dictionary.TryGetValue(makeKey, out var logoUrl) || logoUrl is null)
                {
                    logoUrl = placeholderUrl;
                }

                var make = new UnifiedMakeDto(
                    Name: mk.Name,
                    Models: models,
                    LogoUrl: logoUrl
                );

                return make;
            }).ToList();

        var aggregatedResponses = new UnifiedCarListDto(makes);

        return aggregatedResponses;
    }
}