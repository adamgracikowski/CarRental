using Ardalis.Specification;
using CarRental.Comparer.Persistence.Data;
using CarRental.Comparer.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Comparer.Persistence;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<CarRentalComparerDbContext>();

        services.AddScoped(typeof(IRepositoryBase<>), typeof(CarRentalComparerRepository<>));
        return services;
    }
}