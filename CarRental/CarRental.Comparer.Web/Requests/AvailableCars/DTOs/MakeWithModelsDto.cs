namespace CarRental.Comparer.Web.Requests.AvailableCars.DTOs;

public class MakeWithModelsDto
{
    public string Name { get; set; }

    public ICollection<ModelWithCarsDto> Models { get; set; }
}
