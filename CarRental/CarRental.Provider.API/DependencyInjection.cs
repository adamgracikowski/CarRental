using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Provider.Infrastructure.Calculators.OfferCalculator;
using CarRental.Provider.Persistence.Options;
using FluentValidation;

namespace CarRental.Provider.API;

public static class DependencyInjection
{
    public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));
        services.Configure<OfferCalculatorOptions>(configuration.GetSection(OfferCalculatorOptions.SectionName));

        return services;
    }

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<LoggingMiddleware>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IOfferCalculatorService, OfferCalculatorService>();

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

        services.AddValidatorsFromAssemblyContaining(typeof(Program));

        return services;
    }
}