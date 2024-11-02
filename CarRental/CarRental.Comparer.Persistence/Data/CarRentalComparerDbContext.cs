using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.Persistence.Configurations;
using CarRental.Comparer.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarRental.Comparer.Persistence.Data;

public sealed class CarRentalComparerDbContext : DbContext
{
    public CarRentalComparerDbContext(IOptions<ConnectionStringsOptions> options)
    {
        _options = options;
    }

    private readonly IOptions<ConnectionStringsOptions> _options;

    public DbSet<CarDetails> CarDetails { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Provider> Providers { get; set; }

    public DbSet<RentalTransaction> RentalTransactions { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_options.Value.DefaultConnection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarDetailsConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
