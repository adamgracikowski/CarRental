namespace CarRental.Comparer.Infrastructure.Cache;

public interface ICacheKeyGenerator
{
	string GenerateCarsKey();

	string GenerateTokenKey(string name);

	string GenerateRentalHistoryKey(string email, string status);
}