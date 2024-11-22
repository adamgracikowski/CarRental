using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;

namespace CarRental.Comparer.Persistence.Specifications.Users;

public class UserByEmailWithRentalsByStatusWithCarProviderSpecification : Specification<User>
{
	public UserByEmailWithRentalsByStatusWithCarProviderSpecification(string email, RentalStatus rentalStatus)
	{
		Query.Where(u => u.Email == email)
			.Include(u => u.RentalTransactions.Where(r => r.Status == rentalStatus))
			.ThenInclude(r => r.CarDetails)
			.Include(u => u.RentalTransactions.Where(r => r.Status == rentalStatus))
			.ThenInclude(r => r.Provider);

	}
}