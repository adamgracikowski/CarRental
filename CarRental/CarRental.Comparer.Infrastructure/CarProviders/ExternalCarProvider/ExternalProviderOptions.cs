using CarRental.Comparer.Infrastructure.CarProviders.Options;

namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider;

public sealed class ExternalProviderOptions : ProviderOptionsBase
{
	public const string SectionName = "CarProviders:ExternalProvider";

	public string ApiKey { get; set; } = string.Empty;

	public string HttpKeySectionName { get; set; } = string.Empty;
}