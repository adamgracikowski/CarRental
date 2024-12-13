using CarRental.Provider.API.Authorization.TrustedClientService;

namespace CarRental.Provider.API.Authorization.JwtTokenService;

public sealed class JwtSettingsOptions
{
    public const string SectionName = "JwtSettings";

    public string Issuer { get; set; } = string.Empty;

    public string IssuerSigningKey { get; set; } = string.Empty;

    public int TokenExpirationInMinutes { get; set; }

    public required ICollection<TrustedClient> TrustedClients { get; set; }
}