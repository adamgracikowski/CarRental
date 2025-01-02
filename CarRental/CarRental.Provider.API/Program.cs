using Azure.Identity;
using CarRental.Common.Infrastructure.Configurations;
using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Provider.API;
using CarRental.Provider.API.Authorization;
using CarRental.Provider.API.Swagger;
using CarRental.Provider.Persistence;
using CarRental.Provider.Persistence.Data;
using Hangfire;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
	builder.Configuration.AddUserSecrets<Program>();
}
else
{
	var keyVaultUri = builder.Configuration["AzureKeyVault:VaultUri"];
	ArgumentException.ThrowIfNullOrEmpty(keyVaultUri);

	builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
}

builder.Services.RegisterConfigurationOptions(builder.Configuration);

builder.Services.ConfigureCors(builder.Configuration);
builder.Services.ConfigureAuthorization(builder.Configuration);

builder.Services.RegisterPersistenceServices();
builder.Services.ConfigureHangFire(builder.Configuration);
builder.Services.RegisterInfrastructureServices(builder.Configuration);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerDocumentation();

builder.ConfigureLogger(builder.Environment.IsDevelopment());

var app = builder.Build();

app.AutoRunMigrations<CarRentalProviderDbContext>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<LoggingMiddleware>();

app.UseHangfireDashboard(/* configure dashboard authorization */);

app.UseHttpsRedirection();
app.UseCors(CorsConfiguration.TrustedClientsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();