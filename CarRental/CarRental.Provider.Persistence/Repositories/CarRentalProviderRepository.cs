using Ardalis.Specification.EntityFrameworkCore;

namespace CarRental.Provider.Persistence.Repositories;

public sealed class CarRentalProviderRepository<T> : RepositoryBase<T>
    where T : class
{
    public CarRentalProviderDbContext DbContext { get; set; }

    public CarRentalProviderRepository(CarRentalProviderDbContext dbContext) 
        : base(dbContext)
    {
        this.DbContext = dbContext;
    }
}