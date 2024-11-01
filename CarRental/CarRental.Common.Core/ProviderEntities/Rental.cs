using CarRental.Common.Core.Enums;

namespace CarRental.Common.Core.ProviderEntities;

public sealed class Rental : EntityBase
{
    public required Offer Offer { get; set; }

    public int OfferId { get; set; }

    public required Customer Customer { get; set; }

    public int CustomerId { get; set; }

    public RentalReturn? RentalReturn { get; set; }

    public int? RentalReturnId { get; set; }

    public RentalStatus Status { get; set; }
}