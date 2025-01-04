namespace CarRental.Comparer.Web.Settings;

public sealed class AppSecrets
{
	public AuthSettings? Auth { get; set; }

	public BlazorAzureAdSettings? AzureAd { get; set; }

	public ComparerApiSettings? ComparerApiSettings { get; set; }

	public GoogleMapsSettings? GoogleMaps { get; set; }
}