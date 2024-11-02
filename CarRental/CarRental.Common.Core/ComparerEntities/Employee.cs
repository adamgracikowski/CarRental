namespace CarRental.Common.Core.ComparerEntities;

public sealed class Employee : EntityBase
{
    public string Email { get; set; }

    public int ProviderId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Provider Provider { get; set; }
}
