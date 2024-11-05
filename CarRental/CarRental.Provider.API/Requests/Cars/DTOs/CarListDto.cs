using CarRental.Provider.API.Requests.Makes.DTOs;

namespace CarRental.Provider.API.Requests.Cars.DTOs;

public sealed record CarListDto(ICollection<MakeDto> Makes);