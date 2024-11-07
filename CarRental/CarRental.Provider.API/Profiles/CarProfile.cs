using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.Requests.Cars.DTOs;

namespace CarRental.Provider.API.Profiles;

public sealed class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarDetailsDto>();
    }
}