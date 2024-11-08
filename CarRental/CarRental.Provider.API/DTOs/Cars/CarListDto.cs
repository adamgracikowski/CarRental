using CarRental.Provider.API.DTOs.Makes;

namespace CarRental.Provider.API.DTOs.Cars;

public sealed record CarListDto(ICollection<MakeDto> Makes);