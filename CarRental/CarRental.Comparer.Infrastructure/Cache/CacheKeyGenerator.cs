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

	public string GenerateRentalHistoryKey(string email, string status)
	{
		return $":rentals:{email}:{status}";
	}
}