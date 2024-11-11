using CarRental.Provider.API.Authorization.TrustedClientService;

namespace CarRental.Provider.API.Authorization.JwtTokenService;

public interface IJwtTokenService
{
    string GenerateJwtToken(TrustedClient trustedClient);
}