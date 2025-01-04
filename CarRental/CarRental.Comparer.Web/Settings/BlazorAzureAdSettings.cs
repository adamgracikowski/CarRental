namespace CarRental.Comparer.Web.Settings;

public sealed class BlazorAzureAdSettings
{
	public string ClientId { get; set; } = string.Empty;

	public string Authority { get; set; } = string.Empty;

	public bool ValidateAuthority { get; set; }

	public string ApiScope { get; set; } = string.Empty;
}