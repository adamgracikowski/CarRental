using CarRental.Common.Core.Enums;

namespace CarRental.Comparer.Infrastructure.CarProviders.RentalStatusConversions;

public interface IRentalStatusConverter
{
	bool AreEquivalent(RentalStatus rentalStatus, string outerStatus, string providerName);

	bool TryConvertFromProviderRentalStatus(string outerStatus, string providerName, out RentalStatus rentalStatus);

	bool TryConvertToProviderRentalStatus(RentalStatus rentalStatus, string providerName, out string outerStatus);
}