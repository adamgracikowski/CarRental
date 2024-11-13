namespace CarRental.Comparer.API.Authorization;

public static class CorsConfiguration
{
    public const string TrustedComparerPolicy = "TrustedComparerPolicy";

    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var trustedUrl = configuration.GetValue<string>("TrustedComparer:BaseUrl");

        ArgumentNullException.ThrowIfNull(trustedUrl, $"Trusted Url cannot be null.");
        
        services.AddCors(options =>
        {
            options.AddPolicy(TrustedComparerPolicy, builder =>
            {
                builder.WithOrigins(trustedUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}