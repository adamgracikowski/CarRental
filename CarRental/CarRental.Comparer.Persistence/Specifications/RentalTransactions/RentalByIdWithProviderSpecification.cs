using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;

namespace CarRental.Comparer.Persistence.Specifications.RentalTransactions;

public sealed class RentalByIdWithProviderSpecification : Specification<RentalTransaction>
{
	public RentalByIdWithProviderSpecification(int rentalId)
	{
		Query.Where(r => r.Id == rentalId)
			 .Include(r => r.Provider);
	}
}
