using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Cars;

namespace CarRental.Provider.API.Profiles;

public sealed class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarDetailsDto>();

        CreateMap<Car, CarDto>();

        CreateMap<Car, CarDetailsExtendedDto>()
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
            .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Model.Make.Name));
	}
}