using CarRental.Common.Core.Enums;

namespace CarRental.Common.Core.ComparerEntities;

public sealed class RentalTransaction : EntityBase
{
    public int UserId { get; set; }

    public int ProviderId { get; set; }

    public string RentalOuterId { get; set; }

    public decimal RentalPricePerDay { get; set; }

    public decimal InsurancePricePerDay { get; set; }

    public DateTime RentedAt { get; set; }

    public DateTime? ReturnedAt { get; set; }

    public int CarDetailsId { get; set; }

    public RentalStatus Status { get; set; }

    public User User { get; set; }

    public Provider Provider { get; set; }

    public CarDetails CarDetails { get; set; }
}
