using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Offers;

public sealed class OfferByIdWithRentalSpecification : Specification<Offer>
{
    public OfferByIdWithRentalSpecification(int id)
    {
        Query.Where(o => o.Id == id)
            .Include(o => o.Rental);
    }
}