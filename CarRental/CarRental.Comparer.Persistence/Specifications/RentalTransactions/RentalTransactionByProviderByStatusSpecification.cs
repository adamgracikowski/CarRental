using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;

namespace CarRental.Comparer.Persistence.Specifications.RentalTransactions;

public sealed class RentalTransactionByProviderByStatusSpecification : Specification<RentalTransaction>
{
	public RentalTransactionByProviderByStatusSpecification(int providerId, RentalStatus rentalStatus)
	{
		Query.Where(r => r.ProviderId == providerId)
			.Where(r => r.Status == rentalStatus)
			.OrderByDescending(r => r.RentedAt)
			.Include(r => r.User)
			.Include(r => r.CarDetails);
	}
}