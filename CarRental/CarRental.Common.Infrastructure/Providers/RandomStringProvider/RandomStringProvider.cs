namespace CarRental.Common.Infrastructure.Providers.RandomStringProvider;

public sealed class RandomStringProvider : IRandomStringProvider
{    
    public string GenerateRandomString()
    {
        return Guid.NewGuid().ToString();
    }
}
