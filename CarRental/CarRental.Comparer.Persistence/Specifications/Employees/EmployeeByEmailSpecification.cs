using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;

namespace CarRental.Comparer.Persistence.Specifications.Employees;
public sealed class EmployeeByEmailSpecification : Specification<Employee>
{
	public EmployeeByEmailSpecification(string email)
	{
		Query.Where(e => e.Email == email);
	}
}