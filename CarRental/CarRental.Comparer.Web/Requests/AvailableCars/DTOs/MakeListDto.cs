namespace CarRental.Comparer.Web.Requests.AvailableCars.DTOs;

public sealed record MakeListDto
{
    public ICollection<MakeWithModelsDto> makes { get; set; }
}
