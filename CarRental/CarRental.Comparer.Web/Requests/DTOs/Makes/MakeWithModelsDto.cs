using CarRental.Comparer.Web.Requests.DTOs.Models;

namespace CarRental.Comparer.Web.Requests.DTOs.Makes;

public sealed record MakeWithModelsDto(
    string Name,
    ICollection<ModelWithCarsDto> Models,
    string LogoUrl
);