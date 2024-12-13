using CarRental.Common.Core.Enums;
using CarRental.Comparer.Infrastructure.CarProviders.Options;
using Microsoft.Extensions.Options;

namespace CarRental.Comparer.Infrastructure.CarProviders.RentalStatusConversions;

public sealed class RentalStatusConverter : IRentalStatusConverter
{
	private readonly RentalStatusConversionOptions options;

	public RentalStatusConverter(
		IOptions<RentalStatusConversionOptions> options)
	{
		this.options = options.Value;
	}

	public bool AreEquivalent(RentalStatus rentalStatus, string outerStatus, string providerName)
	{
		var rentalStatusString = this.GetRentalStatusString(outerStatus, providerName);

		return rentalStatus.ToString().Equals(rentalStatusString, StringComparison.OrdinalIgnoreCase);
	}

	public bool TryConvertFromProviderRentalStatus(string outerStatus, string providerName, out RentalStatus rentalStatus)
	{
		var rentalStatusString = this.GetRentalStatusString(outerStatus, providerName);

		return Enum.TryParse(rentalStatusString, ignoreCase: true, out rentalStatus);
	}

	public bool TryConvertToProviderRentalStatus(RentalStatus rentalStatus, string providerName, out string outerStatus)
	{
		outerStatus = string.Empty;

		if (!this.options.Conversions.TryGetValue(providerName, out var providerConversions)) 
			return false;

		var reversedConversions = providerConversions.ToDictionary(p => p.Value, p => p.Key);

		if (!reversedConversions.TryGetValue(rentalStatus.ToString(), out var outerStatusString)) 
			return false;

		outerStatus = outerStatusString;

		return true;
	}

	private string GetRentalStatusString(string outerStatus, string providerName)
	{
		outerStatus = outerStatus.ToLower();

		if (!this.options.Conversions.TryGetValue(providerName, out var providerConversions)) 
			return string.Empty;

		if (!providerConversions.TryGetValue(outerStatus, out var rentalStatusString)) 
			return string.Empty;

		return rentalStatusString;
	}
}