namespace CarRental.Comparer.API.Settings;

public sealed class BlazorAzureAdSettings
{
	public const string SectionName = "BlazorAzureAd";

	public required string ClientId { get; set; }
	
	public required string Authority { get; set; }
	
	public bool ValidateAuthority { get; set; }
	
	public required string ApiScope { get; set; }
}