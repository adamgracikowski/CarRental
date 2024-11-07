using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Cars;

public sealed class CarByIdWithModelSegmentInsuranceSpecification : Specification<Car>
{
    public CarByIdWithModelSegmentInsuranceSpecification(int id)
    {
        Query.Where(c => c.Id == id)
            .Include(c => c.Model)
            .ThenInclude(m => m.Segment)
            .ThenInclude(s => s.Insurance);
    }
}