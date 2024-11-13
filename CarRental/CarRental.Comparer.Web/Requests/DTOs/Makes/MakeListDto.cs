namespace CarRental.Comparer.Web.Requests.DTOs.Makes;

public sealed record MakeListDto(ICollection<MakeWithModelsDto> Makes);