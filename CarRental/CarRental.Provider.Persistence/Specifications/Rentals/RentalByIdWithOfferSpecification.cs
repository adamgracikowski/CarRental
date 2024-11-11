using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Rentals;

public sealed class RentalByIdWithOfferSpecification : Specification<Rental>
{
    public RentalByIdWithOfferSpecification(int id)
    {
        Query.Where(r => r.Id == id)
            .Include(r => r.Offer);
    }
}