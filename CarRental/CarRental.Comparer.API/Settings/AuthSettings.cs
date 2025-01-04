namespace CarRental.Comparer.API.Settings;

public sealed class AuthSettings
{
	public const string SectionName = "Auth";

	public required string Authority { get; set; }
	
	public required string ClientId { get; set; }
	
	public required string PostLogoutRedirectUrl { get; set; }
	
	public required string RedirectUrl { get; set; }
	
	public required string ResponseType { get; set; }
}