using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;

namespace CarRental.Comparer.Persistence.Specifications.Users;

public class UserByEmailSpecification : Specification<User>
{
    public UserByEmailSpecification(string email)
    {
        Query.Where(u => u.Email == email);
    }
}