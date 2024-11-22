namespace CarRental.Comparer.Web.PagesNavModels;

public sealed record HomeToRentalCars
{
	public string SelectedMake { get; set; } = string.Empty;

	public string SelectedModel { get; set; } = string.Empty;
}
