using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CarRental.Provider.API.Swagger;

public static class SwaggerConfiguration
{
	public static IServiceCollection ConfigureSwaggerDocumentation(this IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Car Rental Provider API",
				Version = "v1",
				Description = "## About:\n" +
							  "This API has been developed as a part of the " +
							  "Web Application Development Using .NET Framework academic course " +
							  "during the 2024-2025 academic year.\n\n" +
							  "## Developers:\n" +
							  "- Antonina Frąckowiak\n" +
							  "- Adam Grącikowski\n" +
							  "- Marcin Gronicki",
			});

			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			options.IncludeXmlComments(xmlPath);
		});

		services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

		return services;
	}
}