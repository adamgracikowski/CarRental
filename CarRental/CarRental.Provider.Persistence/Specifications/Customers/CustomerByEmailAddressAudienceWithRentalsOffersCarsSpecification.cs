using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Customers;
public sealed class CustomerByEmailAddressAudienceWithRentalsOffersCarsSpecification : Specification<Customer>
{
	public CustomerByEmailAddressAudienceWithRentalsOffersCarsSpecification(string emailAddress, string audience)
	{
		Query.AsNoTracking()
			.Where(c => c.EmailAddress == emailAddress)
			.Include(c => c.Rentals.Where(r => r.Offer.GeneratedBy == audience))
			.ThenInclude(r => r.Offer)
			.ThenInclude(o => o.Car);
	}
}