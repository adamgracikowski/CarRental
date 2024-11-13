namespace CarRental.Comparer.Infrastructure.CarProviders.Options;

public abstract class ProviderOptionsBase
{
    public string Name { get; set; } = string.Empty;

    public string BaseUrl { get; set; } = string.Empty;
}