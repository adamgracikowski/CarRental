namespace CarRental.Comparer.Infrastructure.CarProviders.Authorization;

public interface ITokenService
{
    Task<string> GetTokenAsync(CancellationToken cancellationToken);
}