using Ardalis.Specification;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Cars;

public sealed class CarsByStatusWithModelMakeSpecification : Specification<Car>
{
    public CarsByStatusWithModelMakeSpecification(CarStatus status)
    {
        Query.Where(c => c.Status == status)
            .Include(c => c.Model)
            .ThenInclude(m => m.Make);
    }
}