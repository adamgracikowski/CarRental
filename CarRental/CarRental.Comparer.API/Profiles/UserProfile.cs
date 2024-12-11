using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.Users;

namespace CarRental.Comparer.API.Profiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>()
            .ReverseMap();

        CreateMap<User, UserIdDto>();

		CreateMap<User, UserForEmployeeDto>()
	        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name));
	}
}