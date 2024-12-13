using Azure.Storage.Blobs;
using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Common.Infrastructure.PipelineBehaviours;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Common.Infrastructure.Storages.BlobStorage;
using CarRental.Comparer.API.Authorization.Roles;
using CarRental.Comparer.API.BackgroundJobs.RentalServices;
using CarRental.Comparer.Infrastructure.Cache;
using CarRental.Comparer.Infrastructure.CarComparisons;
using CarRental.Comparer.Infrastructure.CarProviders;
using CarRental.Comparer.Infrastructure.CarProviders.Authorization;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders;
using CarRental.Comparer.Infrastructure.CarProviders.Options;
using CarRental.Comparer.Infrastructure.CarProviders.RentalStatusConversions;
using CarRental.Comparer.Infrastructure.Reports;
using CarRental.Comparer.Infrastructure.Reports.ExcelReports;
using CarRental.Comparer.Persistence.Options;
using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace CarRental.Comparer.API;

public static class DependencyInjection
{
    public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));
        services.Configure<BlobContainersOptions>(configuration.GetSection(BlobContainersOptions.SectionName));
        services.Configure<RedisOptions>(configuration.GetSection(RedisOptions.SectionName));

        services.Configure<CarProvidersOptions>(configuration.GetSection(CarProvidersOptions.SectionName));
        services.Configure<InternalProviderOptions>(configuration.GetSection(InternalProviderOptions.SectionName));
        services.Configure<RentalStatusConversionOptions>(configuration.GetSection(RentalStatusConversionOptions.SectionName));

        return services;
    }

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<LoggingMiddleware>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.RegisterAutoMapper();
        services.ConfigureMediatR();
        services.ConfigureBlobStorage(configuration);
        services.ConfigureRedis(configuration);
        services.AddMemoryCache();

        services.AddSingleton<ITokenService, InternalProviderTokenService>();
        services.AddTransient<TokenAuthorizationHandler>();
        services.AddTransient<ICarComparisonService, CarComparisonService>();
        services.AddTransient<ICarProviderService, InternalCarProviderService>();
        services.AddAuth(configuration);

        services.AddScoped<IRentalComparerStatusCheckerService, RentalComparerStatusCheckerService>();
        services.AddScoped<IRentalStatusConverter, RentalStatusConverter>();

        services.AddScoped<IPeriodicReportService, ExcelReportService>();
        services.AddSingleton<FileExtensionContentTypeProvider>();

        return services;
    }

    public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(option =>
        {
            option.Configuration = configuration.GetValue<string>("Redis:ConnectionString");
            option.InstanceName = configuration.GetValue<string>("Redis:InstanceName");
        });
        services.AddSingleton<ICacheService, RedisCacheService>();
        services.AddSingleton<ICacheKeyGenerator, CacheKeyGenerator>();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                configuration.Bind("AzureAd", options);
                options.TokenValidationParameters.RoleClaimType = "roles";
            },
            options => { configuration.Bind("AzureAd", options); });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationRoles.Employee, policy => policy.RequireClaim(ClaimTypes.Role, AuthorizationRoles.Employee));
            options.AddPolicy(AuthorizationRoles.User, policy => policy.RequireClaim(ClaimTypes.Role, AuthorizationRoles.User));
        });

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
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

		return services;
    }

    public static IServiceCollection ConfigureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(x => new BlobServiceClient(configuration.GetValue<string>("AzureBlobStorage:ConnectionString")));
        services.AddSingleton<IBlobStorageService, BlobStorageService>();

        return services;
    }

    public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
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
}