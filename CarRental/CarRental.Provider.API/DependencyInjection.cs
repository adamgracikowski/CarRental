using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Provider.Persistence.Options;

namespace CarRental.Provider.API;

public static class DependencyInjection
{
    public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));

        return services;
    }

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<LoggingMiddleware>();

        services.RegisterAutoMapper();
        services.ConfigureMediatR();

        return services;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
        return services;
    }

    public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configurations =>
        {
            configurations.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        return services;
    }
}