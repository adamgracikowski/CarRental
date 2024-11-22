using CarRental.Comparer.Infrastructure.CarProviders;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders;

namespace CarRental.Comparer.Infrastructure.Cache;

public interface ICacheKeyGenerator
{
	string GenerateCarsKey();

	string GenerateTokenKey(string name);

	string GenerateRentalHistoryKey(string email, string status);
}