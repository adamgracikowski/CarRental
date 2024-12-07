using Microsoft.AspNetCore.Http;

namespace CarRental.Common.Infrastructure.Storages.BlobStorage;

public interface IBlobStorageService
{
	Task<string?> UploadFileAsync(string containerName, string fileName, IFormFile file, CancellationToken cancellationToken = default);

	Task<bool> DeleteFileAsync(string containerName, string fileName, CancellationToken cancellationToken = default);

	Task<Stream?> DownloadFileAsync(string containerName, string fileName, CancellationToken cancellationToken = default);

	Task<IEnumerable<string>> GetFilesAsync(string containerName, CancellationToken cancellationToken = default);
}