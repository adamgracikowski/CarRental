using CarRental.Common.Infrastructure.Configurations;
using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Comparer.API;
using CarRental.Comparer.API.Authorization;
using CarRental.Comparer.API.Swagger;
using CarRental.Comparer.Infrastructure.HttpClients;
using CarRental.Comparer.Persistence;
using CarRental.Comparer.Persistence.Data;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.RegisterConfigurationOptions(builder.Configuration);
builder.Services.ConfigureCors(builder.Configuration);

builder.Services.RegisterPersistenceServices();
builder.Services.ConfigureHttpClients(builder.Configuration);
builder.Services.ConfigureHangfire(builder.Configuration);
builder.Services.RegisterInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerDocumentation();

builder.Host.ConfigureLogger(builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.AutoRunMigrations<CarRentalComparerDbContext>();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();

app.UseHangfireDashboard(/* configure dashboard authorization */);

app.UseHttpsRedirection();
app.UseCors(CorsConfiguration.TrustedComparerPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();