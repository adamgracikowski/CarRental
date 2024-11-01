namespace CarRental.Common.Core.ProviderEntities;

public sealed class Insurance : EntityBase
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal PricePerDay { get; set; }

    public ICollection<Segment> Segments { get; set; } = [];
}