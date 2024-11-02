using Ardalis.Specification.EntityFrameworkCore;
using CarRental.Comparer.Persistence.Data;

namespace CarRental.Comparer.Persistence.Repositories;

public sealed class CarRentalComparerRepository<T> : RepositoryBase<T> where T : class
{
    public CarRentalComparerDbContext DbContext { get; set; }

    public CarRentalComparerRepository(CarRentalComparerDbContext dbContext) : base(dbContext)
    {
        this.DbContext = dbContext;
    }
}