namespace CarRental.Common.Core.ComparerEntities;

public sealed class Employee : EntityBase
{
    public required string Email { get; set; }

    public int ProviderId { get; set; }

    public required string FirstName { get; set; }

	public required string LastName { get; set; }

    public required Provider Provider { get; set; }
}