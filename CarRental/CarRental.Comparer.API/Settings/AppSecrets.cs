namespace CarRental.Comparer.API.Settings;

public sealed class AppSecrets
{
	public required AuthSettings? Auth { get; set; }
	
	public required BlazorAzureAdSettings? AzureAd { get; set; }
	
	public required ComparerApiSettings? ComparerApiSettings { get; set; }
	
	public required GoogleMapsSettings? GoogleMaps { get; set; }
}