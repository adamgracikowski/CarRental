using CarRental.Provider.API.Authorization.JwtTokenService;
using Microsoft.Extensions.Options;

namespace CarRental.Provider.API.Authorization.TrustedClientService;

public sealed class TrustedClientService : ITrustedClientService
{
    private readonly IEnumerable<TrustedClient> trustedClients;

    public TrustedClientService(IOptions<JwtSettingsOptions> options)
    {
        this.trustedClients = options.Value.TrustedClients;
    }

    public TrustedClient? ValidateTrustedClient(string clientId, string clientSecretKey)
    {
        return trustedClients.FirstOrDefault(t => t.ClientId == clientId && t.ClientSecretKey == clientSecretKey);
    }
}