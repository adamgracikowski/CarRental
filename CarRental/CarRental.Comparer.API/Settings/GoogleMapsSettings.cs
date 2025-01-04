namespace CarRental.Comparer.API.Settings;

public sealed class GoogleMapsSettings
{
	public const string SectionName = "GoogleMaps";

	public required string ApiKey { get; set; }
}