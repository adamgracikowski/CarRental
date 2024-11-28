namespace CarRental.Common.Core.ComparerEntities;

public sealed class Provider : EntityBase
{
    public required string Name { get; set; }

    public ICollection<RentalTransaction> RentalTransactions { get; set; } = [];

    public ICollection<Employee> Employees { get; set; } = [];
}