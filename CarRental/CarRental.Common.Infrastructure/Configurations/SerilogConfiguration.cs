using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace CarRental.Common.Infrastructure.Configurations;

public static class SerilogConfiguration
{
    public static void ConfigureLogger(this WebApplicationBuilder builder, bool isDevelopment)
    {
        builder.Logging.ClearProviders();

        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationId()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console();

        if (isDevelopment)
        {
            loggerConfiguration.WriteTo.File(
               path: "Logs/CarRental_.txt",
               rollingInterval: RollingInterval.Day,
               retainedFileCountLimit: 7,
               fileSizeLimitBytes: 100_000,
               rollOnFileSizeLimit: true);
        }
        else
        {
            var applicationInsightsConnectionString = builder
                .Configuration["ApplicationInsights:InstrumentationKey"];

            ArgumentException.ThrowIfNullOrEmpty(applicationInsightsConnectionString);

            loggerConfiguration.WriteTo.ApplicationInsights(
                applicationInsightsConnectionString,
                TelemetryConverter.Traces,
                restrictedToMinimumLevel: LogEventLevel.Information
            );
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        builder.Host.UseSerilog();
    }
}