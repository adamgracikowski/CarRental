using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;

namespace CarRental.Comparer.Persistence.Specifications.RentalTransactions;

public sealed class RentalTransactionByStatusProviderIdDateTimeRangeWithCarDetailsSpecification : Specification<RentalTransaction>
{
	public RentalTransactionByStatusProviderIdDateTimeRangeWithCarDetailsSpecification(
		DateTime dateFrom, 
		DateTime dateTo, 
		RentalStatus status, 
		int providerId)
	{
		Query.Where(
			r => r.ProviderId == providerId &&
			dateFrom <= r.RentedAt &&
			r.ReturnedAt <= dateTo &&
			r.Status == status
		).Include(r => r.CarDetails)
		 .AsNoTracking();
	}
}