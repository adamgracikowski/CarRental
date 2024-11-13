using CarRental.Comparer.Infrastructure.CarProviders.Options;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders;

public sealed class InternalProviderOptions : ProviderOptionsBase
{
    public const string SectionName = "CarProviders:InternalProvider";

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;

    public int TokenNearExpirationMinutes { get; set; }
}
