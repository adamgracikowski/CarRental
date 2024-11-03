using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Rentals;

public sealed class RentalByIdSpecification : Specification<Rental>
{
    public RentalByIdSpecification(int id)
    {
        Query.Where(r => r.Id == id);
    }
}