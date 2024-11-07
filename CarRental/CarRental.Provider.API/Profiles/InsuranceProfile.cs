using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.Requests.Insurances.DTOs;

namespace CarRental.Provider.API.Profiles;

public sealed class InsuranceProfile : Profile
{
    public InsuranceProfile()
    {
        CreateMap<Insurance, InsuranceDto>();
    }
}