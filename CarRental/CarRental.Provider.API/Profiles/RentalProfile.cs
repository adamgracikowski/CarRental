using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.Requests.Rentals.DTOs;

namespace CarRental.Provider.API.Profiles;

public sealed class RentalProfile : Profile
{
    public RentalProfile()
    {
        CreateMap<Rental, RentalStatusDto>();
    }
}