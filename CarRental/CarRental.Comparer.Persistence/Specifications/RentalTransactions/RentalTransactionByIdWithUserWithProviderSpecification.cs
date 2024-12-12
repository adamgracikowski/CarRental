using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;

namespace CarRental.Comparer.Persistence.Specifications.RentalTransactions;

public sealed class RentalTransactionByIdWithUserWithProviderSpecification : Specification<RentalTransaction>
{
	public RentalTransactionByIdWithUserWithProviderSpecification(int rentalId)
	{
		Query.Where(r => r.Id == rentalId)
			 .Include(r => r.Provider)
			 .Include(r => r.User);
	}
}