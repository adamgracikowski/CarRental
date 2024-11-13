using CarRental.Common.Infrastructure.Configurations;
using CarRental.Common.Infrastructure.Middlewares;
using CarRental.Comparer.API;
using CarRental.Comparer.API.Authorization;
using CarRental.Comparer.Infrastructure.HttpClients;
using CarRental.Comparer.Persistence;
using CarRental.Comparer.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddUserSecrets<Program>();

builder.Services.RegisterConfigurationOptions(builder.Configuration);
builder.Services.ConfigureCors(builder.Configuration);

builder.Services.RegisterPersistenceServices(builder.Configuration);
builder.Services.ConfigureHttpClients(builder.Configuration);
builder.Services.RegisterInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureLogger(builder.Environment.IsDevelopment());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.AutoRunMigrations<CarRentalComparerDbContext>();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();
app.UseCors(CorsConfiguration.TrustedComparerPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();