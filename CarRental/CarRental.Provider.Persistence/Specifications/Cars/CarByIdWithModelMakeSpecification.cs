using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Cars;

public sealed class CarByIdWithModelMakeSpecification : Specification<Car>
{
	public CarByIdWithModelMakeSpecification(int id)
	{
		Query.AsNoTracking()
			.Where(c => c.Id == id)
			.Include(c => c.Model)
			.ThenInclude(m => m.Make);
	}
}