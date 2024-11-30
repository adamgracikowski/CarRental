namespace CarRental.Comparer.Infrastructure.CarProviders.Options;
public sealed class RentalStatusConversionOptions
{
	public const string SectionName = "RentalStatusConversion";

	public Dictionary<string, Dictionary<string, string>> Conversions { get; set; } = [];
}