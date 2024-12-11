using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.RentalTransactions;

namespace CarRental.Comparer.API.Profiles;

public sealed class RentalTransactionProfile : Profile
{
	public RentalTransactionProfile()
	{
		CreateMap<RentalTransaction, RentalTransactionDto>()
			.ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.CarDetails.Make))
			.ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.CarDetails.Model))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

		CreateMap<RentalTransaction, RentalTransactionForEmployeeDto>()
			.ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.CarDetails));
	}
}