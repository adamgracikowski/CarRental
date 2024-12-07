using CarRental.Common.Core.ComparerEntities;
using AutoMapper;
using CarRental.Comparer.API.DTOs.RentalTransactions;

namespace CarRental.Comparer.API.Profiles;

public sealed class RentalTransactionForEmployeeProfile : Profile
{
	public RentalTransactionForEmployeeProfile()
	{
		CreateMap<RentalTransaction, RentalTransactionForEmployeeDto>()
			.ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.CarDetails));
	}
}