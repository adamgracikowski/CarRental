using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Rentals;

public sealed class RentalByIdWithOfferCarSpecification : Specification<Rental>
{
    public RentalByIdWithOfferCarSpecification(int id)
    {
        Query.Where(r => r.Id == id)
            .Include(r => r.Offer)
            .ThenInclude(o => o.Car);
    }
}