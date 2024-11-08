using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Cars;

namespace CarRental.Provider.API.Profiles;

public sealed class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarDetailsDto>();
    }
}