using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders;

namespace CarRental.Comparer.Infrastructure.CarProviders.Options;

public sealed class CarProvidersOptions
{
    public const string SectionName = "CarProviders";

    public required InternalProviderOptions InternalProvider { get; set; }
}