namespace CarRental.Provider.Infrastructure.Storages.BlobStorage;

public sealed class BlobContainersOptions
{
    public const string SectionName = "AzureBlobStorage:BlobContainers";

    public string RentalReturnsContainer { get; set; } = string.Empty;
}