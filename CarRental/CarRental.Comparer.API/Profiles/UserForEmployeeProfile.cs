using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.Users;

namespace CarRental.Comparer.API.Profiles;

public sealed class UserForEmployeeProfile : Profile
{
	public UserForEmployeeProfile()
	{
		CreateMap<User, UserForEmployeeDto>()
			.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name));
	}
}