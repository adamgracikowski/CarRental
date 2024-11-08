using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Rentals;

public sealed class RentalByIdWithRentalReturnSpecification : Specification<Rental>
{
    public RentalByIdWithRentalReturnSpecification(int id)
    {
        Query.Where(r => r.Id == id)
            .Include(r => r.RentalReturn);
    }
}