using Azure.Storage.Blobs;
using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Provider.Infrastructure.BackgroundJobs.RentalServices;
using CarRental.Provider.Infrastructure.Calculators.OfferCalculator;
using CarRental.Provider.Infrastructure.Storages.BlobStorage;
using CarRental.Provider.Persistence.Options;
using FluentValidation;
using Hangfire;

namespace CarRental.Provider.API;

public static class DependencyInjection
{
    public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));
        services.Configure<OfferCalculatorOptions>(configuration.GetSection(OfferCalculatorOptions.SectionName));
        services.Configure<BlobContainersOptions>(configuration.GetSection(BlobContainersOptions.SectionName));

        return services;
    }

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<LoggingMiddleware>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IOfferCalculatorService, OfferCalculatorService>();
        services.AddScoped<IRentalStatusCheckerService, RentalStatusCheckerService>();

        services.RegisterAutoMapper();
        services.ConfigureMediatR();
        services.ConfigureBlobStorage(configuration);

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

    public static IServiceCollection ConfigureHangFire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"))
                .UseColouredConsoleLogProvider()
                .UseSerilogLogProvider();
        });

        services.AddHangfireServer();

        return services;
    }

    public static IServiceCollection ConfigureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(x => new BlobServiceClient(configuration.GetValue<string>("AzureBlobStorage:ConnectionString")));
        services.AddSingleton<IBlobStorageService, BlobStorageService>();

        return services;
    }
}