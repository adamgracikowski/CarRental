namespace CarRental.Common.Core.ComparerEntities;

public sealed class User : EntityBase
{
	public required string Email { get; set; }

	public required string Name { get; set; }

	public required string Lastname { get; set; }

	public DateTime Birthday { get; set; }

	public DateTime DrivingLicenseDate { get; set; }

	public double Longitude { get; set; }

	public double Latitude { get; set; }

	public ICollection<RentalTransaction> RentalTransactions { get; set; } = [];
}