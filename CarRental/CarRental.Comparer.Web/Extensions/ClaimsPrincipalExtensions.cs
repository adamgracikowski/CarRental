using System.Security.Claims;

namespace CarRental.Comparer.Web.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetClaimValue(this ClaimsPrincipal user, string claimType)
    {
        return user?.Claims?.FirstOrDefault(c => c.Type == claimType)?.Value;
    }

    public static string? GetName(this ClaimsPrincipal user)
    {
        return user.GetClaimValue("name");
    }

    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user.GetClaimValue("email");
    }
}