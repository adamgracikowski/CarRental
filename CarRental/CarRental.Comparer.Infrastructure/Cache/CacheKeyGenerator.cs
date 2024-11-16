using CarRental.Comparer.Infrastructure.CarProviders;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders;

namespace CarRental.Comparer.Infrastructure.Cache;

public sealed class CacheKeyGenerator : ICacheKeyGenerator
{
    public string GenerateCarsKey()
    {
        return $":cars:";
    }

    public string GenerateTokenKey(string name)
    {
        return $":token:{name}";
    }
}