using Ardalis.Specification;
using CarRental.Provider.Persistence.Data;
using CarRental.Provider.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Provider.Persistence;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<CarRentalProviderDbContext>();

        services.AddScoped(typeof(IRepositoryBase<>), typeof(CarRentalProviderRepository<>));
        return services;
    }
}