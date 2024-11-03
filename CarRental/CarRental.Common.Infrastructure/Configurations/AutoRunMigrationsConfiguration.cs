using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Common.Infrastructure.Configurations;

public static class AutoRunMigrationConfiguration
{
    public static void AutoRunMigrations<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var context = GetDbContext<TContext>(serviceScope);
        var migrationsTimeoutInMinutes = GetMigrationsTimeout(serviceScope);

        context.Database.SetCommandTimeout(TimeSpan.FromMinutes(migrationsTimeoutInMinutes));

        RunMigrations(context);
    }

    private static TContext GetDbContext<TContext>(IServiceScope serviceScope)
        where TContext : DbContext
    {
        var context = serviceScope.ServiceProvider.GetService<TContext>()
            ?? throw new InvalidOperationException($"{nameof(TContext)} not registered. Ensure it is configured correctly.");

        return context;
    }

    private static int GetMigrationsTimeout(IServiceScope serviceScope)
    {
        var migrationsTimeoutConfig = serviceScope.ServiceProvider.GetService<IConfiguration>()?.GetValue<int?>("Migrations:TimeoutInMinutes");
        return migrationsTimeoutConfig ?? InfrastructureConfigurationConstants.DefaultMigrationsTimeoutInMinutes;
    }

    private static void RunMigrations<TContext>(TContext context)
        where TContext : DbContext

    {
        using var transaction = context.Database.BeginTransaction();

        try
        {
            context.Database.Migrate();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw new InvalidOperationException("Database migration failed.", ex);
        }
    }
}