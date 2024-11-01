using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.Persistence.Configurations;
using CarRental.Provider.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarRental.Provider.Persistence.Repositories;

public sealed class CarRentalProviderDbContext : DbContext
{
    private readonly IOptions<ConnectionStringsOptions> options;

    public DbSet<Car> Cars { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Insurance> Insurances { get; set; }

    public DbSet<Make> Makes { get; set; }

    public DbSet<Model> Models { get; set; }

    public DbSet<Offer> Offers { get; set; }

    public DbSet<Rental> Rentals { get; set; }

    public DbSet<RentalReturn> RentalReturns { get; set; }

    public DbSet<Segment> Segments { get; set; }

    public CarRentalProviderDbContext(IOptions<ConnectionStringsOptions> options)
    {
        this.options = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(this.options.Value.DefaultConnection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InsuranceConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}