using CarRental.Comparer.Persistence.Options;

namespace CarRental.Comparer.API;

public static class DependencyInjection
{
    public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));

        return services;
    }
}
