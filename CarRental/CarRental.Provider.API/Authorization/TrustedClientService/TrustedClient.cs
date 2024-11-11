namespace CarRental.Provider.API.Authorization.TrustedClientService;

public sealed class TrustedClient
{
    public string ClientId { get; set; } = string.Empty;

    public string ClientSecretKey { get; set; } = string.Empty;

    public string BaseUrl { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;
}