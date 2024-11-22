namespace CarRental.Comparer.API.DTOs.RentalTransactions;

public sealed record RentalTransactionDto
{
	public string Make { get; set; } = string.Empty;
	public string Model { get; set; } = string.Empty;
	public int Id { get; set; }
	public string Status { get; set; } = string.Empty;
	public DateTime RentedAt { get; set; }
	public DateTime? ReturnedAt { get; set; }
	public decimal RentalPricePerDay { get; set; }
	public decimal InsurancePricePerDay { get; set; }
}