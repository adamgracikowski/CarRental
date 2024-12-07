namespace CarRental.Provider.API.DTOs.RentalReturns;

public sealed record RentalReturnDto
{
	public int Id { get; set; }
	public string? Image { get; set; }
}