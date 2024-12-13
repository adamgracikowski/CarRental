using System.Security.Claims;

namespace CarRental.Comparer.API.Authorization;

public static class ClaimsPrincipalExtensions
{
	private const string EmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

	public static string? GetEmailClaim(this ClaimsPrincipal claimsPrincipal)
	{
		return claimsPrincipal.Claims
			.FirstOrDefault(c => c.Type == EmailClaim)?.Value;
	}
}