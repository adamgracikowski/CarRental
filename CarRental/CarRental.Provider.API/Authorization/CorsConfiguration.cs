using CarRental.Provider.API.Authorization.JwtTokenService;

namespace CarRental.Provider.API.Authorization;

public static class CorsConfiguration
{
    public const string TrustedClientsPolicy = "TrustedClientsPolicy";

    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(JwtSettingsOptions.SectionName).Get<JwtSettingsOptions>();

        ArgumentNullException.ThrowIfNull(jwtSettings, $"{JwtSettingsOptions.SectionName} cannot be null.");

        if (jwtSettings.TrustedClients == null || jwtSettings.TrustedClients.Count == 0)
        {
            throw new ArgumentNullException($"{nameof(JwtSettingsOptions.TrustedClients)} cannot be null or empty.");
        }

        var trustedOrigins = jwtSettings.TrustedClients
            .Select(t => t.BaseUrl)
            .ToArray();

        services.AddCors(options =>
        {
            options.AddPolicy(TrustedClientsPolicy, builder =>
            {
                builder.WithOrigins(trustedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}