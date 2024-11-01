namespace CarRental.Common.Core.ProviderEntities;

public sealed class RentalReturn : EntityBase
{
    public required Rental Rental { get; set; }

    public int RentalId { get; set; }

    public DateTime ReturnedAt { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}