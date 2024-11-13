using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Rentals;

public sealed class RentalByIdWithOfferCarModelMakeClientSpecification: Specification<Rental>
{
    public RentalByIdWithOfferCarModelMakeClientSpecification(int id)
    {
        Query.Where(r => r.Id == id)
            .Include(r => r.Offer)
            .ThenInclude(o => o.Car)
            .ThenInclude(mo => mo.Model)
            .ThenInclude(m => m.Make)
            .Include(r => r.Customer);
    }
}
