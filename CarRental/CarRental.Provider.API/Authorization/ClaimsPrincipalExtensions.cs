using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarRental.Provider.API.Authorization;

public static class ClaimsPrincipalExtensions
{
	public static string? GetAudience(this ClaimsPrincipal claimsPrincipal)
	{
		return claimsPrincipal.Claims
			.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Aud)?.Value;
	}
}