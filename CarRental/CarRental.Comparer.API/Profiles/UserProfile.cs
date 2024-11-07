using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.Users.DTOs;

namespace CarRental.Comparer.API.Profiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ReverseMap();
        CreateMap<User, UserDto>();
    }
}