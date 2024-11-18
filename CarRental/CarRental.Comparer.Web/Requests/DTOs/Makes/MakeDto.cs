using CarRental.Comparer.Web.Requests.DTOs.Models;

namespace CarRental.Comparer.Web.Requests.DTOs.Makes;

public sealed record MakeDto(
    string Name,
    ICollection<ModelDto> Models,
    string LogoUrl
);