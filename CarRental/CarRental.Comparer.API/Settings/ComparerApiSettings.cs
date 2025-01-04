namespace CarRental.Comparer.API.Settings;

public sealed class ComparerApiSettings
{
	public const string SectionName = "ComparerApiSettings";

	public required string BaseUrl { get; set; }
}