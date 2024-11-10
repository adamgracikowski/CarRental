namespace CarRental.Common.Core.ProviderEntities;

public sealed class Offer : EntityBase
{
    public required Car Car { get; set; }

    public int CarId { get; set; }

    public DateTime GeneratedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public decimal RentalPricePerDay { get; set; }

    public decimal InsurancePricePerDay { get; set; }

    public Rental? Rental { get; set; }

    public int? RentalId { get; set; }

    public required string Key { get; set; }
}