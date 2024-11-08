using Microsoft.AspNetCore.Http;

namespace CarRental.Provider.Infrastructure.Storages.BlobStorage;

public interface IBlobStorageService
{
    Task<bool> UploadFileAsync(string containerName, string fileName, IFormFile file, CancellationToken cancellationToken = default);

    Task<bool> DeleteFileAsync(string containerName, string fileName, CancellationToken cancellationToken = default);

    Task<Stream?> DownloadFileAsync(string containerName, string fileName, CancellationToken cancellationToken = default);
}