using CarRental.Common.Infrastructure.Configurations;
using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Provider.API;
using CarRental.Provider.API.Authorization;
using CarRental.Provider.API.Swagger;
using CarRental.Provider.Persistence;
using CarRental.Provider.Persistence.Data;
using Hangfire;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

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

builder.Host.ConfigureLogger(builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.AutoRunMigrations<CarRentalProviderDbContext>();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();

app.UseHangfireDashboard(/* configure dashboard authorization */);

app.UseHttpsRedirection();
app.UseCors(CorsConfiguration.TrustedClientsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();