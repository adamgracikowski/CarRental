using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Common.Infrastructure.Storages.BlobStorage;
using CarRental.Comparer.API.DTOs;
using CarRental.Comparer.API.DTOs.CarLogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CarRental.Comparer.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class CarLogosController : ControllerBase
{
    private readonly IBlobStorageService blobStorageService;
    private readonly BlobContainersOptions options;
    private readonly string StorageAccountName = string.Empty;

    public CarLogosController(
        IBlobStorageService blobStorageService,
        IOptions<BlobContainersOptions> options,
        IConfiguration configuration)
    {
        this.blobStorageService = blobStorageService;
        this.options = options.Value;
        StorageAccountName = configuration.GetValue<string>($"AzureBlobStorage:StorageAccountName") ?? string.Empty;
    }

    [TranslateResultToActionResult]
    [HttpGet]
    public async Task<Result<CarLogoCollectionDto>> GetCarLogos(CancellationToken cancellationToken)
    {
        var files = await this.blobStorageService.GetFilesAsync(options.MakeLogosContainer, cancellationToken);

        var carLogoDtos = files.Select(file =>
        {
            var make = Path.GetFileNameWithoutExtension(file);
            var logoUrl = $"https://{StorageAccountName}.blob.core.windows.net/{options.MakeLogosContainer}/{file}";
            return new CarLogoDto(make, logoUrl);
        }).ToList();

        var carLogosCollectionDto = new CarLogoCollectionDto(carLogoDtos);

        return Result<CarLogoCollectionDto>.Success(carLogosCollectionDto);
    }
}