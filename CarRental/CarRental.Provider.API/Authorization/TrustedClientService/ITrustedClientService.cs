namespace CarRental.Provider.API.Authorization.TrustedClientService;

public interface ITrustedClientService
{
    TrustedClient? ValidateTrustedClient(string clientId, string clientSecret);
}