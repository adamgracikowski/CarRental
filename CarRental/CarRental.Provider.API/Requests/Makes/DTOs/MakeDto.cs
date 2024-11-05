using CarRental.Provider.API.Requests.Models.DTOs;

namespace CarRental.Provider.API.Requests.Makes.DTOs;

public sealed record MakeDto(string Name, ICollection<ModelDto> Models);