using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Rentals;

public sealed class RentalByIdWithRentalReturnOfferCarSpecification : Specification<Rental>
{
    public RentalByIdWithRentalReturnOfferCarSpecification(int id)
    {
        Query.Where(r => r.Id == id)
            .Include(r => r.RentalReturn)
            .Include(r => r.Offer)
            .ThenInclude(o => o.Car);
    }
}