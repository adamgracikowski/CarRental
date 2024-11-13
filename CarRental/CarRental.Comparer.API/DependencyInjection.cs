using Azure.Storage.Blobs;
using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Common.Infrastructure.Storages.BlobStorage;
using CarRental.Comparer.Persistence.Options;

namespace CarRental.Comparer.API;

public static class DependencyInjection
{
    public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));
        services.Configure<BlobContainersOptions>(configuration.GetSection(BlobContainersOptions.SectionName));

        return services;
    }

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<LoggingMiddleware>();

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

        return services;
    }

    public static IServiceCollection ConfigureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(x => new BlobServiceClient(configuration.GetValue<string>("AzureBlobStorage:ConnectionString")));
        services.AddSingleton<IBlobStorageService, BlobStorageService>();

        return services;
    }
}