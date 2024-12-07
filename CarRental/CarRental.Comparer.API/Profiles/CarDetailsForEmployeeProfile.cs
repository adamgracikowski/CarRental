using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.CarDetails;

namespace CarRental.Comparer.API.Profiles;

public sealed class CarDetailsForEmployeeProfile : Profile
{
	public CarDetailsForEmployeeProfile()
	{
		CreateMap<CarDetails, CarDetailsForEmployeeDto>();
	}
}