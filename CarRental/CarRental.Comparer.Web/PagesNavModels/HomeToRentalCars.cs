using CarRental.Comparer.Web.Requests.AvailableCars.DTOs;

namespace CarRental.Comparer.Web.PagesNavModels;

public sealed record HomeToRentalCars
{
    public ICollection<MakeWithModelsDto> Makes { get; set; }

    public string SelectedMake { get; set; }

    public string SelectedModel { get; set; }

    public HomeToRentalCars(ICollection<MakeWithModelsDto> makes)
    {
        Makes = makes;
    }
}
