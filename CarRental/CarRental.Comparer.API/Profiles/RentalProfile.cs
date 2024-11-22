using AutoMapper;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.RentalTransactions;

namespace CarRental.Comparer.API.Profiles;

public sealed class RentalProfile : Profile
{
	public RentalProfile()
	{
		CreateMap<RentalTransaction, RentalTransactionDto>()
			.ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.CarDetails.Make))
			.ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.CarDetails.Model))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
	}
}