using Azure.Storage.Blobs;
using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Common.Infrastructure.Providers.RandomStringProvider;
using CarRental.Common.Infrastructure.Storages.BlobStorage;
using CarRental.Provider.API.Authorization.JwtTokenService;
using CarRental.Provider.API.Authorization.TrustedClientService;
using CarRental.Provider.Infrastructure.BackgroundJobs.RentalServices;
using CarRental.Provider.Infrastructure.Calculators.OfferCalculator;
using CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;
using CarRental.Provider.Infrastructure.EmailService;
using CarRental.Provider.Infrastructure.EmailService.Options;
using CarRental.Provider.Infrastructure.EmailService.TemplateProviders;
using CarRental.Provider.Persistence.Options;
using FluentValidation;
using Hangfire;
using SendGrid;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CarRental.Provider.API;

public static class DependencyInjection
{
    public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));
        services.Configure<OfferCalculatorOptions>(configuration.GetSection(OfferCalculatorOptions.SectionName));
        services.Configure<BlobContainersOptions>(configuration.GetSection(BlobContainersOptions.SectionName));
        services.Configure<JwtSettingsOptions>(configuration.GetSection(JwtSettingsOptions.SectionName));
        services.Configure<SendEmailOptions>(configuration.GetSection(SendEmailOptions.SectionName));
        services.Configure<JwtSettingsOptions>(configuration.GetSection(JwtSettingsOptions.SectionName));

        return services;
    }

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<LoggingMiddleware>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IOfferCalculatorService, OfferCalculatorService>();
        services.AddTransient<IRentalBillCalculatorService, RentalBillCalculatorService>();
        services.AddScoped<IRentalStatusCheckerService, RentalStatusCheckerService>();
        services.AddSingleton<IRandomStringProvider, RandomStringProvider>();
        
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ITrustedClientService, TrustedClientService>();

        services.RegisterAutoMapper();
        services.ConfigureMediatR();
        services.ConfigureBlobStorage(configuration);
        services.ConfigureSendGrid(configuration);

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

    public static IServiceCollection ConfigureSendGrid(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.ConfigureEmailTemplates(configuration);
        services.AddScoped<IEmailInputMaker, EmailInputMaker>();
        services.AddSingleton(x => new SendGridClient(configuration.GetValue<string>("SendGrid:APIKey")));

        return services;
    }

    public static IServiceCollection ConfigureEmailTemplates(this IServiceCollection services, IConfiguration configuration)
    {
        var confirmOfferContent = GetEmbeddedResourceContent("SendGrid:EmbeddedResources:OfferConfirmedTemplate", configuration);

        var rentalConfirmationContent = GetEmbeddedResourceContent("SendGrid:EmbeddedResources:RentalConfirmedTemplate", configuration);

        var rentalReturnedContent = GetEmbeddedResourceContent("SendGrid:EmbeddedResources:RentalReturnedTemplate", configuration);

        var rentalReturnStartedContent = GetEmbeddedResourceContent("SendGrid:EmbeddedResources:RentalReturnStartedTemplate", configuration);

        services.AddSingleton(x => new OfferConfirmedTemplate(confirmOfferContent));

        services.AddSingleton(x => new RentalReturnConfirmedTemplate(rentalReturnedContent));

        services.AddSingleton(x => new RentalConfirmedTemplate(rentalConfirmationContent));

        services.AddSingleton(x => new RentalReturnStartedTemplate(rentalReturnStartedContent));

        return services;
    }

    private static string GetEmbeddedResourceContent(string resourceKey, IConfiguration configuration)
    {
        var path = configuration.GetValue<string>(resourceKey);

        if (path == null)
        {
            throw new ArgumentNullException("Error when retrieving value from options.");
        }

        using var stream = Assembly.GetEntryAssembly()?.GetManifestResourceStream(path);

        if (stream == null)
        {
            throw new Exception("Error while loading template");
        }

        using var reader = new StreamReader(stream, System.Text.Encoding.UTF8);

        return reader.ReadToEnd();

    }
}