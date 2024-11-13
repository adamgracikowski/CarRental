namespace CarRental.Common.Infrastructure.Storages.BlobStorage;

public sealed class BlobContainersOptions
{
    public const string SectionName = "AzureBlobStorage:BlobContainers";

    public string RentalReturnsContainer { get; set; } = string.Empty;
    public string MakeLogosContainer { get; set; } = string.Empty;
}