using CarRental.Common.Infrastructure.Storages.BlobStorage;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs;
using CarRental.Comparer.Infrastructure.CarProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CarRental.Comparer.Infrastructure.CarComparisons;

public sealed class CarComparisonService : ICarComparisonService
{
    private readonly IEnumerable<ICarProviderService> carProviderServices;
    private readonly IBlobStorageService blobStorageService;
    private readonly BlobContainersOptions options;
    private readonly string StorageAccountName = string.Empty;

    public CarComparisonService(
        IEnumerable<ICarProviderService> carProviderServices,
        IBlobStorageService blobStorageService,
        IOptions<BlobContainersOptions> options,
        IConfiguration configuration)
    {
        this.carProviderServices = carProviderServices;
        this.blobStorageService = blobStorageService;
        this.options = options.Value;

        StorageAccountName = configuration.GetValue<string>($"AzureBlobStorage:StorageAccountName") ?? string.Empty;
    }

    public async Task<UnifiedCarListDto> GetAllAvailableCarsAsync(CancellationToken cancellationToken)
    {
        var tasks = this.carProviderServices.Select(provider => provider.GetAvailableCarsAsync(cancellationToken));
        
        var results = await Task.WhenAll(tasks);
        
        var aggregatedResponse = await this.AggregateResponsesAsync(results, cancellationToken);

        return aggregatedResponse;
    }

    private async Task<UnifiedCarListDto> AggregateResponsesAsync(IEnumerable<UnifiedCarListDto?> responses, CancellationToken cancellationToken)
    {
        var files = await this.blobStorageService.GetFilesAsync(options.MakeLogosContainer, cancellationToken);

        var dictionary = files.ToDictionary(
            f => Path.GetFileNameWithoutExtension(f),
            f => $"https://{StorageAccountName}.blob.core.windows.net/{options.MakeLogosContainer}/{f}"
        );

        var placeholder = "placeholder";

        if(!dictionary.TryGetValue(placeholder, out var placeholderUrl) || placeholderUrl is null)
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

                if(!dictionary.TryGetValue(makeKey, out var logoUrl) || logoUrl is null)
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