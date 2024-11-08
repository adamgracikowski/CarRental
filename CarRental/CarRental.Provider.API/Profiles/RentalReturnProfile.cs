using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.RentalReturns;

namespace CarRental.Provider.API.Profiles;

public sealed class RentalReturnProfile : Profile
{
    public RentalReturnProfile()
    {
        CreateMap<RentalReturn, RentalReturnDto>();
    }
}