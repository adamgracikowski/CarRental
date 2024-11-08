using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Customers;

public sealed class CustomerByEmailAddressSpecification : Specification<Customer>
{
    public CustomerByEmailAddressSpecification(string emailAddress)
    {
        Query.Where(c => c.EmailAddress == emailAddress);
    }
}