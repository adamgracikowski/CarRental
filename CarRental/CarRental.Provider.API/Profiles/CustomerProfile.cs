using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Customers;

namespace CarRental.Provider.API.Profiles;

public sealed class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerDto, Customer>();
    }
}