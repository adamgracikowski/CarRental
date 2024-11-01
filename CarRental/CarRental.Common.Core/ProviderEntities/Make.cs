namespace CarRental.Common.Core.ProviderEntities;

public sealed class Make : EntityBase
{
    public required string Name { get; set; }

    public ICollection<Model> Models { get; set; } = [];
}