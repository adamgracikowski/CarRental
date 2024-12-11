using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace CarRental.Comparer.API.Authorization;

public static class ClaimsPrincipalExtensions
{
	private static readonly string emailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
	public static string? GetEmailClaim(this ClaimsPrincipal claimsPrincipal)
	{
		return claimsPrincipal.Claims
			.FirstOrDefault(c => c.Type == emailClaim)?.Value;
	}
}