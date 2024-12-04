using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;

namespace CarRental.Comparer.Persistence.Specifications.Employees;
public sealed class EmployeeByEmailWithProviderSpecification : Specification<Employee>
{
	public EmployeeByEmailWithProviderSpecification(string email)
	{
		Query.Where(e => e.Email == email)
			.Include(e => e.Provider);
	}
}