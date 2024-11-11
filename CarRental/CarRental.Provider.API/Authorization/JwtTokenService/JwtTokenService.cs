using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Provider.API.Authorization.TrustedClientService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRental.Provider.API.Authorization.JwtTokenService;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly JwtSettingsOptions options;

    public JwtTokenService(
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtSettingsOptions> options)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.options = options.Value;
    }

    public string GenerateJwtToken(TrustedClient trustedClient)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, trustedClient.ClientId),
            new Claim(JwtRegisteredClaimNames.Aud, trustedClient.Audience),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: trustedClient.Audience,
            claims: claims,
            expires: dateTimeProvider.UtcNow.AddMinutes(options.TokenExpirationInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}