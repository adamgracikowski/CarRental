using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Offers;

public sealed class OfferByIdWithRentalCarModelMakeSpecification : Specification<Offer>
{
    public OfferByIdWithRentalCarModelMakeSpecification(int id)
    {
        Query.Where(o => o.Id == id)
            .Include(o => o.Rental)
            .Include(o => o.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Make);
    }
}