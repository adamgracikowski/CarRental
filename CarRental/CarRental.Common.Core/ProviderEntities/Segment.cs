namespace CarRental.Common.Core.ProviderEntities;

public sealed class Segment : EntityBase
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal PricePerDay { get; set; }

    public required Insurance Insurance { get; set; }

    public int InsuranceId { get; set; }

    public ICollection<Model> Models { get; set; } = [];
}