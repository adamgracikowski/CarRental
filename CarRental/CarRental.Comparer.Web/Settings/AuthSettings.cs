namespace CarRental.Comparer.Web.Settings;

public sealed class AuthSettings
{
	public string Authority { get; set; } = string.Empty;

	public string ClientId { get; set; } = string.Empty;

	public string PostLogoutRedirectUrl { get; set; } = string.Empty;

	public string RedirectUrl { get; set; } = string.Empty;

	public string ResponseType { get; set; } = string.Empty;
}