using Microsoft.AspNetCore.Components.Forms;

namespace CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

public sealed class AcceptRentalReturnDto
{
	public IBrowserFile? Image { get; set; }
	public string Description { get; set; } = string.Empty;
	public double Latitude { get; set; }
	public double Longitude { get; set; }
}