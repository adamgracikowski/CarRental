using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Comparer.Infrastructure.CarProviders.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders;

public sealed class InternalProviderTokenService : ITokenService
{
    private record AuthResponseDto(string Token);
    private record AuthRequestDto(string ClientId, string ClientSecret);

    private readonly IHttpClientFactory httpClientFactory;
    private readonly IMemoryCache cache; // redis in the future
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly InternalProviderOptions options;

    public InternalProviderTokenService(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache,
        IOptions<InternalProviderOptions> options,
        IDateTimeProvider dateTimeProvider)
    {
        this.httpClientFactory = httpClientFactory;
        this.cache = cache;
        this.dateTimeProvider = dateTimeProvider;
        this.options = options.Value;
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        var precision = TimeSpan.FromMinutes(options.TokenNearExpirationMinutes);

        if (cache.TryGetValue<string>(options.Name, out var token) &&
            token is not null &&
            !IsTokenNearExpiration(token, precision))
        {
            return token;
        }

        token = await RequestNewTokenAsync(cancellationToken);
        var expirationTime = GetExpirationTime(token);

        if (expirationTime.HasValue)
        {
            cache.Set(options.Name, token, absoluteExpiration: expirationTime.Value);
        }

        return token;
    }

    private async Task<string> RequestNewTokenAsync(CancellationToken cancellationToken)
    {
        using var httpClient = httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(options.BaseUrl);

        var request = new AuthRequestDto(options.ClientId, options.ClientSecret);

        var response = await httpClient.PostAsJsonAsync("Auth/token", request, cancellationToken);
        
        var responseDto = await response.Content.ReadFromJsonAsync<AuthResponseDto>(cancellationToken);

        return responseDto?.Token ?? string.Empty;
    }

    private DateTimeOffset? GetExpirationTime(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        if (handler.CanReadToken(token))
        {
            var jwtToken = handler.ReadJwtToken(token);
            var expirationClaim = jwtToken.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);

            if (expirationClaim != null && long.TryParse(expirationClaim.Value, out var expirationValue))
            {
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expirationValue);
                return expirationTime;
            }
        }

        return null;
    }

    private bool IsTokenNearExpiration(string token, TimeSpan precision)
    {
        var expirationTime = GetExpirationTime(token);

        return expirationTime.HasValue && expirationTime.Value.UtcDateTime <= dateTimeProvider.UtcNow.Add(precision);
    }
}