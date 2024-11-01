namespace CarRental.Common.Core.ProviderEntities;

public sealed class Customer : EntityBase
{
    public required string EmailAddress { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public ICollection<Rental> Rentals { get; set; } = [];
}