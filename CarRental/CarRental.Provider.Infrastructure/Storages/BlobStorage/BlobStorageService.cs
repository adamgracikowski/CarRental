using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarRental.Provider.Infrastructure.Storages.BlobStorage;

public sealed class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient blobServiceClient;
    private readonly ILogger logger;

    public BlobStorageService(
        BlobServiceClient blobServiceClient,
        ILogger<BlobStorageService> logger)
    {
        this.blobServiceClient = blobServiceClient;
        this.logger = logger;
    }

    public async Task<bool> DeleteFileAsync(string containerName, string fileName, CancellationToken cancellationToken = default)
    {
        try
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            var blobClient = containerClient.GetBlobClient(fileName);

            var result = await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);

            if (result)
            {
                this.logger.LogInformation("File {FileName} successfully deleted from container {ContainerName}.", fileName, containerName);
            }
            else
            {
                this.logger.LogInformation("File {FileName} not found in container {ContainerName}.", fileName, containerName);
            }

            return true;
        }
        catch (Exception ex)
        {
            this.logger.LogInformation("Error while deleting file {FileName} from container {ContainerName}: {ErrorMessage}.", fileName, containerName, ex.Message);
            return false;
        }
    }

    public async Task<Stream?> DownloadFileAsync(string containerName, string fileName, CancellationToken cancellationToken = default)
    {
        try
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);

            if (!await containerClient.ExistsAsync(cancellationToken: cancellationToken))
            {
                this.logger.LogInformation("Container {ContainerName} does not exist.", containerName);
                return null;
            }

            var blobClient = containerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync(cancellationToken: cancellationToken))
            {
                this.logger.LogInformation("File {FileName} does not exist in container {ContainerName}.", fileName, containerName);
                return null;
            }

            var download = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

            this.logger.LogInformation("File {FileName} successfully downloaded from container {ContainerName}.", fileName, containerName);
            return download.Value.Content.ToStream();
        }
        catch (Exception ex)
        {
            this.logger.LogInformation("Error while downloading file {FileName} from container {ContainerName}: {ErrorMessage}.", fileName, containerName, ex.Message);
            return null;
        }
    }

    public async Task<bool> UploadFileAsync(string containerName, string fileName, IFormFile file, CancellationToken cancellationToken = default)
    {
        try
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            var blobClient = containerClient.GetBlobClient(fileName);
            var blobHttpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            using var stream = file.OpenReadStream();

            await blobClient.UploadAsync(stream, blobHttpHeaders, cancellationToken: cancellationToken);

            this.logger.LogInformation("File {FileName} successfully uploaded to container {ContainerName}.", fileName, containerName);
            return true;
        }
        catch (Exception ex)
        {
            this.logger.LogInformation("Error while uploading file {FileName} to container {ContainerName}: {ErrorMessage}.", fileName, containerName, ex.Message);
            return false;
        }
    }
}