using CarRental.Common.Core.Enums;

namespace CarRental.Common.Core.ComparerEntities;

public sealed class RentalTransaction : EntityBase
{
	public int UserId { get; set; }

	public int ProviderId { get; set; }

	public required string RentalOuterId { get; set; }

	public decimal RentalPricePerDay { get; set; }

	public decimal InsurancePricePerDay { get; set; }

	public DateTime RentedAt { get; set; }

	public DateTime? ReturnedAt { get; set; }

	public int CarDetailsId { get; set; }

	public RentalStatus Status { get; set; }

	public required User User { get; set; }

	public required Provider Provider { get; set; }

	public required CarDetails CarDetails { get; set; }

	public string? Description { get; set; }

	public string? Image { get; set; }
}