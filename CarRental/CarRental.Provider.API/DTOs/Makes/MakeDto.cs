using CarRental.Provider.API.DTOs.Models;

namespace CarRental.Provider.API.DTOs.Makes;

public sealed record MakeDto(
	string Name, 
	ICollection<ModelDto> Models
);