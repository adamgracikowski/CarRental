using CarRental.Provider.API.Authorization.JwtTokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarRental.Provider.API.Authorization;

public static class AuthorizationConfiguration
{
	public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = configuration.GetSection(JwtSettingsOptions.SectionName).Get<JwtSettingsOptions>();

		ArgumentNullException.ThrowIfNull(jwtSettings, $"{JwtSettingsOptions.SectionName} cannot be null.");

		if (jwtSettings.TrustedClients == null || jwtSettings.TrustedClients.Count == 0)
		{
			throw new ArgumentNullException($"{nameof(JwtSettingsOptions.TrustedClients)} cannot be null or empty.");
		}

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings.Issuer,
				ValidAudiences = jwtSettings.TrustedClients.Select(t => t.Audience).ToList(),
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey))
			};
		});

		services.AddAuthorization();

		return services;
	}
}