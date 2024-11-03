using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;

namespace CarRental.Common.Infrastructure.Configurations;

public static class SerilogConfiguration
{
    public static void ConfigureLogger(this IHostBuilder hostBuilder, bool isDevelopment)
    {
        hostBuilder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

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
            // application insights...
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        hostBuilder.UseSerilog();
    }
}