using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Rentals;

namespace CarRental.Provider.API.Profiles;

public sealed class RentalProfile : Profile
{
    public RentalProfile()
    {
        CreateMap<Rental, RentalStatusDto>();
        CreateMap<Rental, RentalDto>();
    }
}